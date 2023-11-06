using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System;
using AspNetCore.ReCaptcha;
using Newtonsoft.Json;

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

        [HttpGet()]

        public IActionResult EmailValidationResponse(int id)
        {
            UserProfile model = new UserProfile();
            model = _appDbContext.Profiles.Find(id);
            model.EmailValidate = true;
            _appDbContext.Profiles.Update(model);
            _appDbContext.SaveChanges();
            return View();
        }


        [HttpGet]
        public IActionResult AddAddress(int id = 0)
        {
            AddAddressVM model = new AddAddressVM();
            model.ProfileId = id;
            
            return View(model);
        }


        [HttpGet]
        public IActionResult Address(int id = 0)
        {
            UserAddress userAddress = new UserAddress();
            userAddress = _appDbContext.UserAddresses.Find(id);
            return View(userAddress);
        }


        [HttpGet]
        public IActionResult ProfileIndex(int id=0)
        {
            UserProfile user = new UserProfile();
            user = _appDbContext.Profiles.Find(id);
            return View(user);
        }

        [HttpGet]
        public IActionResult UpdateProfile(int id=0)
        {
            UserProfile userProfile = new UserProfile();
            userProfile = _appDbContext.Profiles.Find(id);
            return View(userProfile);
        }

        [HttpGet]
        public IActionResult UpdatePreferences(int id = 0)
        {
            ProfilePreferences preference = new ProfilePreferences();
            preference = _appDbContext.ProfilePreferencesList.Find(id);
             
            return View(preference);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View(); 
        }

        [HttpGet]
        public IActionResult EmailResetPassword()
        {
            EmailResetPasswordVM password = new EmailResetPasswordVM();
            return View(password);
        }


        [HttpGet()]

        public IActionResult ChangePassword(int id)
        {
            ChangePasswordVM model = new ChangePasswordVM();
            model.ObtainedOldPassword = _appDbContext.Profiles.Find(id).Password;
            model.CurrentProfileId = id;
            return View(model);
        }


        [HttpGet]
        public IActionResult ListAddresses(int id = 0)
        {
            AddressListVM list = new AddressListVM();
            list.ProfileId = id;
            list.AllAddresses = _appDbContext.UserAddresses.OrderBy(g => g.StreetAddress).Where(g=>g.UserProfileId==id).ToList();
            return View(list);
        }

        public IActionResult DeleteAddress(int id = 0)
        {
           
            
            UserAddress address = new UserAddress();
            address = _appDbContext.UserAddresses.Find(id);
            _appDbContext.UserAddresses.Remove(address);
            _appDbContext.SaveChanges();
            return RedirectToAction("ListAddresses",new { id = address.UserProfileId });
    
        }


        [HttpPost] // Handle POST requests
        public IActionResult Register(UserProfile model)
        {
            
            var client = new HttpClient();
            var response = client.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6Lc0Vv4oAAAAACNHmft44sWxMp_kCOOCUjOdMV8b&response={Request.Form["g-recaptcha-response"]}").Result;
            var recaptchaResult = JsonConvert.DeserializeObject<RecaptchaResponse>(response);

            if (!recaptchaResult.Success)
            {
                ModelState.AddModelError("", "reCAPTCHA validation failed. Please prove you are not a robot.");
                return View(model); // Return the view to display the validation error message.
            }

            else
            {
                if (ModelState.IsValid)
                {

                    UserProfile queriedUser = _appDbContext.Profiles.FirstOrDefault(p => p.DisplayName == model.DisplayName);

                    if (queriedUser == null)
                    {
                        _appDbContext.Profiles.Add(model);
                        _appDbContext.SaveChanges();
                        ProfilePreferences preferences = new ProfilePreferences();
                        preferences.UserProfileId = model.Id;
                        _appDbContext.ProfilePreferencesList.Add(preferences);
                        _appDbContext.SaveChanges();

                        string fromAddress = "nirmaldeepak96@gmail.com";
                        string toAddress = model.Email;
                        string link = Url.Action("EmailValidationResponse", "LoginRegister", new { id = model.Id }, protocol: "https");
                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential(fromAddress, "fwmvsjqvkcddiqcw"),
                            EnableSsl = true,
                        };

                        var mailMessage = new MailMessage()
                        {

                            From = new MailAddress(fromAddress),
                            Subject = "Validation Required!",
                            Body = "Your new account has been created. But before you can sign in validation is required. " + $"<a href ={link}>Please click here to validate</a>" + " <br><br>Thanks,<br><br>The Video Game Store team",
                            IsBodyHtml = true
                        };

                        mailMessage.To.Add(toAddress);

                        smtpClient.SendAsync(mailMessage, null);

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
                    ViewData["Message"] = "";
                    return View(model);
                } 
            }
      
        }
        [HttpPost] // Handle POST requests
        public IActionResult Address(UserAddress model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.UserAddresses.Update(model);
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Address has been edited successfully";
                return View(model);
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
        }

        [HttpPost] // Handle POST requests
        public IActionResult AddAddress(AddAddressVM model)
        {
            if (ModelState.IsValid)
            {

                _appDbContext.UserAddresses.Add(model.newUserAddress);
                model.newUserAddress.UserProfileId = model.ProfileId;
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Address has been added successfully";
                return RedirectToAction("ProfileIndex","LoginRegister", new { id = model.ProfileId });
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
        }

        [HttpPost] // Handle POST requests
        public IActionResult UpdateProfile(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Profiles.Update(model);
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Profile details have been updated successfully";
                return View(model);
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
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
                    if (QueriedUserProfile.EmailValidate == true)
                    {
                        if (QueriedUserProfile.RepeatedInvalidCreds < 3)
                        {
                            if (QueriedUserProfile.DisplayName == model.Username && QueriedUserProfile.Password == model.Password)
                            {
                                QueriedUserProfile.RepeatedInvalidCreds = 0;
                                _appDbContext.Profiles.Update(QueriedUserProfile);
                                _appDbContext.SaveChanges();
                                return RedirectToAction("ProfileIndex", "LoginRegister", new { id = QueriedUserProfile.Id });
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
                        ViewData["Message"] = "Please Validate your email";
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

        [HttpPost] // Handle POST requests
        public IActionResult UpdatePreferences(ProfilePreferences model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.ProfilePreferencesList.Update(model);
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Preferences have been updated successfully";
                return View(model);
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult EmailResetPassword(EmailResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                UserProfile user = new UserProfile();
                user = _appDbContext.Profiles.FirstOrDefault(p=>p.DisplayName == model.UserName);

                if (user != null)
                {
                    Random random1 = new Random();              

                    int randomNumber1 = random1.Next(1, 9);
                    int randomNumber2 = random1.Next(1, 9);
                    int randomNumber3 = random1.Next(1, 9);
                    char randomAlphabet1 = (char)('a' + random1.Next(26));
                    char randomAlphabet2 = (char)('A' + random1.Next(26));
                    string characterSet = "@#$%^&+=";
                    char randomCharacter = characterSet[random1.Next(characterSet.Length)];

                    string newPassword = randomNumber1.ToString() + randomNumber2.ToString() + randomNumber3.ToString()+randomAlphabet1+randomAlphabet2+randomCharacter;
                    user.Password = newPassword;
                    _appDbContext.Profiles.Update(user);
                    _appDbContext.SaveChanges();

                    string fromAddress = "nirmaldeepak96@gmail.com";
                    string toAddress = user.Email;
                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential(fromAddress, "fwmvsjqvkcddiqcw"),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage()
                    {

                        From = new MailAddress(fromAddress),
                        Subject = "New Password",
                        Body = "Your password has been reset. Your new password is " +user.Password+ " <br><br>Thanks,<br><br>The Video Game Store team",
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(toAddress);

                    smtpClient.SendAsync(mailMessage, null);

                    ViewData["Message"] = "Check your email for new password";
                    return View();
                }

                else
                {
                    ViewData["Message"] = "User does not exist in the database";
                    return View(model);
                }
               
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }

           
        }

        [HttpPost()]

        public IActionResult ChangePassword(ChangePasswordVM model)
        {

            if (ModelState.IsValid)
            {
                if (model.OldPassword == model.ObtainedOldPassword)
                {
                    if (model.NewPassword == model.ConfirmNewPassword)
                    {
                        UserProfile newProfile = new UserProfile();
                        newProfile = _appDbContext.Profiles.Find(model.CurrentProfileId);
                        newProfile.Password = model.NewPassword;
                        _appDbContext.Profiles.Update(newProfile);
                        _appDbContext.SaveChanges();
                        ViewData["Message"] = "Password has been updated successfully";
                        return RedirectToAction("ProfileIndex", "LoginRegister",new {id = model.CurrentProfileId });
                    }

                    else
                    {
                        ViewData["Message"] = "The new and the confirmed passwords do not match";
                        return View(model);
                    }

                }
                else
                {
                    ViewData["Message"] = "The Old password entered doesnt match the password on the file";
                    return View(model);
                }
            }

            else
            {

                ViewData["Message"] = "";
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
