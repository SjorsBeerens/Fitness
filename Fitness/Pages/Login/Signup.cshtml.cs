using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

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

            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var checkEmailQuery = "SELECT COUNT(1) FROM [User] WHERE Email = @Email";
                using (var checkCommand = new SqlCommand(checkEmailQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Email", Email);
                    var emailExists = (int)checkCommand.ExecuteScalar() > 0;

                    if (emailExists)
                    {
                        ModelState.AddModelError("Email", "This email address is already in use.");
                        return Page();
                    }
                }

                // Hash het wachtwoord
                var passwordHasher = new PasswordHasher<object>();
                var hashedPassword = passwordHasher.HashPassword(null, Password);

                var insertQuery = @"
                INSERT INTO [User] (Name, Email, Password)
                OUTPUT INSERTED.UserID
                VALUES (@Name, @Email, @Password)";
                using (var insertCommand = new SqlCommand(insertQuery, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Name", FullName);
                    insertCommand.Parameters.AddWithValue("@Email", Email);
                    insertCommand.Parameters.AddWithValue("@Password", hashedPassword);

                    var userId = (int)insertCommand.ExecuteScalar();

                    TempData["UserID"] = userId;
                }
            }

            return RedirectToPage("AdditionalInfo");
        }
    }
}
