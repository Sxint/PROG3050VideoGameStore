using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;
using System.Diagnostics;

namespace PROG3050VideoGameStore.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly ILogger<LoginRegisterController> _logger;
        private readonly SignInManager<UserLoginInfo> _signInManager;


        public LoginRegisterController(ILogger<LoginRegisterController> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Address()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(); 
        }

        [HttpPost] // Handle POST requests
        public IActionResult Register(UserProfile model)
        {
            if (ModelState.IsValid)
            {
             
                UserProfile queriedUser = _appDbContext.Profiles.FirstOrDefault(p => p.DisplayName == model.DisplayName);
             
                if (queriedUser == null)
                {
                    _appDbContext.Profiles.Add(model);
                    _appDbContext.SaveChanges();
                    return RedirectToAction("Login", "LoginRegister");
                }

                else
                {
                    ViewData["Message"] = "Choose a different Display Name. Name has already been taken";
                    return View(model);
                }
            }

            else
            {
                ViewData["Message"]="";
                return View(model);
            }
      
        }

        [HttpPost] // Handle POST requests
        public IActionResult Address(UserProfile model)
        {
            return RedirectToAction("Index", "Home");
        }


        [HttpPost] // Handle POST requests
        public IActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                UserProfile QueriedUserProfile = new UserProfile();
                QueriedUserProfile = _appDbContext.Profiles.FirstOrDefault(p=>p.DisplayName==model.Username);
                if (QueriedUserProfile!=null)
                {
                    if (QueriedUserProfile.RepeatedInvalidCreds<3)
                    {
                        if (QueriedUserProfile.DisplayName == model.Username && QueriedUserProfile.Password == model.Password)
                        {
                            QueriedUserProfile.RepeatedInvalidCreds = 0;
                            _appDbContext.Profiles.Update(QueriedUserProfile);
                            return RedirectToAction("Index", "Home"); // This needs to be routed with the associated profile
                        }

                        else
                        {
                            QueriedUserProfile.RepeatedInvalidCreds += 1;
                            _appDbContext.Profiles.Update(QueriedUserProfile);
                            _appDbContext.SaveChanges();
                            ViewData["Message"] = "Invalid Credentials";
                            return View(model);
                        }

                    }

                    else
                    {
                        ViewData["Message"] = "Account Locked Out. Contact Admin";
                        return View(model);
                    }

                }

                else
                {
                    ViewData["Message"] = "Profile doesnt exist";
                    return View(model);
                }
                _appDbContext.SaveChanges();
                return RedirectToAction("Register", "LoginRegister");
            }

            else
            {
                return View(model);
            }
        }





        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private AppDbContext _appDbContext;
    }
}
