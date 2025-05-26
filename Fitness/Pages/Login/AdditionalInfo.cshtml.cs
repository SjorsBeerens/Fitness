using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Fitness.Pages.Login
{
    public class AdditionalInfoModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public AdditionalInfoModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        [Required(ErrorMessage = "Weight is required.")]
        [Range(40, 250, ErrorMessage = "Weight must be between 40 and 250 kg.")]
        public decimal Weight { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Height is required.")]
        [Range(120, 220, ErrorMessage = "Height must be between 120 and 220 cm.")]
        public int Height { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Age is required.")]
        [Range(16, 120, ErrorMessage = "Age must be between 16 and 120.")]
        public int Age { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Activity level is required.")]
        [Range(1, 2, ErrorMessage = "Activity level must be between 1 and 2.")]
        public decimal ActivityLevel { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!TempData.ContainsKey("UserID") || !int.TryParse(TempData["UserID"]?.ToString(), out var userId))
            {
                return RedirectToPage("Signup");
            }

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = @"
            UPDATE [User]
            SET Weight = @Weight,
                Height = @Height,
                Age = @Age,
                Gender = @Gender,
                ActivityLevel = @ActivityLevel
            WHERE UserID = @UserID";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Weight", Weight);
                    command.Parameters.AddWithValue("@Height", Height);
                    command.Parameters.AddWithValue("@Age", Age);
                    command.Parameters.AddWithValue("@Gender", Gender);
                    command.Parameters.AddWithValue("@ActivityLevel", ActivityLevel);
                    command.Parameters.AddWithValue("@UserID", userId);

                    command.ExecuteNonQuery();
                }
            }

            // Gebruik RedirectToPage om naar de loginpagina te gaan
            return RedirectToPage("/Login/Login");
        }
    }
}
