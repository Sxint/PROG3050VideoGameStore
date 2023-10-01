using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG3050VideoGameStore.Models;
using System.Diagnostics;

namespace PROG3050VideoGameStore.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly ILogger<LoginRegisterController> _logger;
        private readonly SignInManager<UserLoginInfo> _signInManager;


        public LoginRegisterController(ILogger<LoginRegisterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View(); 
        }

        [HttpPost] // Handle POST requests
        public IActionResult Register(UserCredentials model)
        {
            return RedirectToAction("Address");
        }

        [HttpPost] // Handle POST requests
        public IActionResult Address(UserCredentials model)
        {
            return RedirectToAction("Index", "Home");
        }


        //[HttpPost] // Handle POST requests
        //public IActionResult Login(AccountInfoViewModel model)
        //{
        //    return RedirectToAction("Index", "Home");
        //}

        [HttpPost]
        public async Task<IActionResult> Login(UserCredentials model)
        {
            // Validate user credentials
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // User is successfully authenticated
                    // The authentication middleware sets User.Identity.IsAuthenticated to true.
                    return RedirectToAction("Index", "Home");
                }
                // Handle failed login
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
