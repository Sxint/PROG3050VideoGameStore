using Microsoft.AspNetCore.Mvc;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;
using System.Diagnostics;

namespace PROG3050VideoGameStore.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ListVM list = new ListVM();
            list.ProfileId = 0;
            list.AllGames = _appDbContext.Games.OrderBy(g => g.Name).ToList();
            return View(list);
        }

        public IActionResult SignOut(int id=0)
        {

            ListVM list = new ListVM();
            list.ProfileId = 0;
            list.AllGames = _appDbContext.Games.OrderBy(g => g.Name).ToList();

            Models.Cart cart = _appDbContext.Carts.FirstOrDefault(c => c.UserProfileId == id);
            List<CartItem> model = _appDbContext.CartItems.Where(c => c.CartId == cart.Id).ToList();

            if (model != null)
            {
                foreach (CartItem item in model)
                {
                    _appDbContext.CartItems.Remove(item);
                } 
            }

            _appDbContext.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private AppDbContext _appDbContext;
    }
}