using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PROG3050VideoGameStore.Controllers
{
    public class WishListController : Controller
    {
        public WishListController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        // GET: /<controller>/

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
            return View("WishList", list);
        }

        public IActionResult WishListForFriends(int profileId = 0, int currentProfileId =0)
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


            WishListForFriendsVM list = new WishListForFriendsVM();
            list.ProfileId = currentProfileId;
            list.FriendProfileId = profileId;
            list.Ratings = _appDbContext.Rating.ToList();
            list.AllGames = new List<Game>();


            if (gameIds != null)
            {
                foreach (int gameId in gameIds)
                {
                    list.AllGames.Add(_appDbContext.Games.Find(gameId));
                }
            }
            return View("WishListForFriends", list);
        }

        public IActionResult RemoveFromWishlist(int id = 0, int profileId = 0)
        {
            WishList alreadyExistingListItem = _appDbContext.WishlistItems.FirstOrDefault(w => w.GameId == id && w.UserProfileId == profileId);

            _appDbContext.WishlistItems.Remove(alreadyExistingListItem);
            _appDbContext.SaveChanges();

            return RedirectToAction("WishList", "WishList", new { id = id, profileId = profileId });

        }

        private AppDbContext _appDbContext;
    }
}

