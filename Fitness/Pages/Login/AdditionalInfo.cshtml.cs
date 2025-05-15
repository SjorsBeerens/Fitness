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
        public decimal Weight { get; set; }

        [BindProperty]
        public int Height { get; set; }

        [BindProperty]
        public int Age { get; set; }

        [BindProperty]
        public string Gender { get; set; }

        [BindProperty]
        public decimal ActivityLevel { get; set; }

        public IActionResult OnPost()
        {
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
