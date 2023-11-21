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

        public IActionResult WishList(int profileId = 0)
        {
            List<WishList> currentProfileWishList = _appDbContext.WishlistItems.Where(w => w.UserProfileId == profileId).ToList();
            List<int> gameIds = new List<int>();
            if (currentProfileWishList != null)
            {
                foreach (WishList wish in currentProfileWishList)
                {
                    gameIds.Add(wish.GameId);
                } 
            }


            AllGamesVM list = new AllGamesVM();
            list.ProfileId = profileId;
            list.Ratings = _appDbContext.Rating.ToList();
            list.AllGames = new List<Game>();


            if (gameIds != null)
            {
                foreach (int gameId in gameIds)
                {
                    list.AllGames.Add(_appDbContext.Games.Find(gameId));
                } 
            }
            return View(list);
        }

        public IActionResult RemoveFromWishlist(int id = 0, int profileId = 0)
        {
            WishList alreadyExistingListItem = _appDbContext.WishlistItems.FirstOrDefault(w => w.GameId == id && w.UserProfileId == profileId);
            
            _appDbContext.WishlistItems.Remove(alreadyExistingListItem);
            _appDbContext.SaveChanges();
            
            return RedirectToAction("WishList","Profile", new {id = id, profileId = profileId});

        }

        private AppDbContext _appDbContext;

    }
}
