using CRUD_Demo.Models;
using CRUD_Demo.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CRUD_Demo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public AccountController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string firstName, string lastName)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName
                };

                var result = await _authRepository.RegisterAsync(user, password);

                if (result.Succeeded)
                {
                    await _authRepository.LoginAsync(email, password);
                    // return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "Employee");

                }
                 
                foreach (var error in result.Errors)
                {
                    if (error.Description.Contains("Passwords"))
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    else if (error.Description.Contains("Email"))
                    {
                        ModelState.AddModelError("Email", error.Description);
                    }
                    else
                    {
                        ModelState.AddModelError("", error.Description); 
                    }
                }


            }
            return View();
        }

        public IActionResult Login()
            {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _authRepository.LoginAsync(email, password);

                if (user != null)
                {
                    // return RedirectToAction("Index", "Home");
                    return RedirectToAction("Index", "Employee");

                }
                
                ModelState.AddModelError("", "Invalid login attempt");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authRepository.LogoutAsync();
            return RedirectToAction("login", "Account");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email is required");
                return View();
            }

            var user = await _authRepository.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "No user found with this email");
                return View();
            }

            var token = await _authRepository.GeneratePasswordResetTokenAsync(user);

            var resetLink = Url.Action("ResetPassword", "Account",
                new { token = token, email = email }, Request.Scheme);

            ViewBag.ResetLink = resetLink;

            return View("ForgotPasswordConfirmation");
        }

        public IActionResult ResetPassword(string token, string email)
        {
            ViewBag.Token = token;
            ViewBag.Email = email;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _authRepository.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View();
            }

            var result = await _authRepository.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View();
        }

    }
}
