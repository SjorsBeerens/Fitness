using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;

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

            // Controleer de inloggegevens
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT UserID FROM [User] WHERE Email = @Email AND Password = @Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password); // Overweeg hashing

                    var userId = command.ExecuteScalar();
                    if (userId != null)
                    {
                        // Maak een sessie aan
                        HttpContext.Session.SetString("UserEmail", Email);
                        HttpContext.Session.SetInt32("UserID", (int)userId);

                        return RedirectToPage("/Dashboard");
                    }
                    else
                    {
                        ErrorMessage = "Invalid email or password.";
                        return Page();
                    }
                }
            }
        }
    }
}
