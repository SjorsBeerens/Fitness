using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

namespace Fitness.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        private readonly IConfiguration _configuration;

        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT UserID, Password FROM [User] WHERE Email = @Email";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var userId = reader["UserID"];
                            var hashedPassword = reader["Password"].ToString();

                            // Vergelijk het ingevoerde wachtwoord met het gehashte wachtwoord
                            var passwordHasher = new PasswordHasher<object>();
                            var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, Password);

                            if (result == PasswordVerificationResult.Success)
                            {
                                // Maak een sessie aan
                                HttpContext.Session.SetString("UserEmail", Email);
                                HttpContext.Session.SetInt32("UserID", (int)userId);

                                return RedirectToPage("/Dashboard");
                            }
                        }
                    }
                }
            }

            ErrorMessage = "Invalid email or password.";
            return Page();
        }

    }
}
