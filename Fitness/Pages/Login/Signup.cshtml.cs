using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using FitnessDAL.Repositories;

namespace Fitness.Pages.Login
{
    public class SignupModel : PageModel
    {
        private readonly UserRepository _userRepository;

        public SignupModel(IConfiguration configuration)
        {
            _userRepository = new UserRepository(configuration.GetConnectionString("DefaultConnection"));
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

            if (_userRepository.IsEmailInUse(Email))
            {
                ModelState.AddModelError("Email", "This email address is already in use.");
                return Page();
            }

            var passwordHasher = new PasswordHasher<object>();
            var hashedPassword = passwordHasher.HashPassword(null, Password);

            var userId = _userRepository.CreateUser(FullName, Email, hashedPassword);
            TempData["UserID"] = userId;

            return RedirectToPage("AdditionalInfo");
        }
    }
}
