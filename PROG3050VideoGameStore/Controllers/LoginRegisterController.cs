using Microsoft.AspNetCore.Mvc;
using PROG3050VideoGameStore.Models;
using System.Diagnostics;

namespace PROG3050VideoGameStore.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly ILogger<LoginRegisterController> _logger;

        public LoginRegisterController(ILogger<LoginRegisterController> logger)
        {
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
