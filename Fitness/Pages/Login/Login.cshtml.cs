using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using FitnessDAL.Repositories;

namespace Fitness.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        private readonly UserRepository _userRepository;

        public LoginModel(IConfiguration configuration)
        {
            _userRepository = new UserRepository(configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var (userId, hashedPassword) = _userRepository.GetUserByEmail(Email);
            if (userId.HasValue)
            {
                var passwordHasher = new PasswordHasher<object>();
                var result = passwordHasher.VerifyHashedPassword(null, hashedPassword, Password);

                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("UserEmail", Email);
                    HttpContext.Session.SetInt32("UserID", userId.Value);

                    return RedirectToPage("/Dashboard");
                }
            }

            ErrorMessage = "Invalid email or password.";
            return Page();
        }
    }
}
