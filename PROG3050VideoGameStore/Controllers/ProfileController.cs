using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult profile(int id=0)
        {
            ProfileVM model = new ProfileVM();
            model.ProfileId = id;
            model.Preference = _appDbContext.ProfilePreferencesList.Include(p => p.Profile).FirstOrDefault(p => p.UserProfileId == id);
            model.AllFriendsList = _appDbContext.FamilyFriendsList.Where(f => (f.UserProfileId == id || f.ReceivedBy == id) && f.Status == "Accepted").ToList();
            model.AllProfiles = model.AllProfiles = _appDbContext.Profiles.ToList();
            model.AllGames = new List<Game>();
            List<Order> orderList = _appDbContext.Orders.Include(o => o.OrderItems).Where(o => o.UserProfileId == id).ToList();

            foreach (Order order in orderList)
            {
                if (order.OrderStatus == "Confirmed")
                {
                    foreach (OrderItem item in order.OrderItems)
                    {
                        Game game = _appDbContext.Games.Find(item.GameId);
                        model.AllGames.Add(game);

                    }
                }

            }

            return View(model);
        }

     


        private AppDbContext _appDbContext;

    }
}
