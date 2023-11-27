using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;

namespace PROG3050VideoGameStore.Controllers
{
    public class Profile : Controller
    {
        public Profile(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult profile(int id)
        {
            //dynamic models: https://www.c-sharpcorner.com/UploadFile/ff2f08/multiple-models-in-single-view-in-mvc/
            dynamic user = new ExpandoObject();
            user.Profile = _appDbContext.Profiles.Find(id);
            user.Preferences = _appDbContext.ProfilePreferencesList.Find(id);
            return View(user);
        }

     


        private AppDbContext _appDbContext;

    }
}
