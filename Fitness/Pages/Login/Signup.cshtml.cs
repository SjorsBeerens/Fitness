using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Fitness.Pages.Login
{
    public class SignupModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public SignupModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return Page();
            }

            // Voeg de gebruiker toe aan de database
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            int userId;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = @"
                    INSERT INTO [User] (Name, Email, Password)
                    OUTPUT INSERTED.UserID
                    VALUES (@Name, @Email, @Password)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", FullName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password); // Overweeg hashing

                    // Haal de gegenereerde UserID op
                    userId = (int)command.ExecuteScalar();
                }
            }

            // Sla de UserID op in TempData
            TempData["UserID"] = userId;

            return RedirectToPage("AdditionalInfo");
        }
    }
}
