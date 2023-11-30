using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;
using System.Security.Cryptography.Xml;

namespace PROG3050VideoGameStore.Controllers
{
    public class Cart : Controller
    {
        public Cart(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult CartPage(int id = 0)
        {
            Models.Cart currentCart = _appDbContext.Carts
                .Include(c => c.CartItems) // Explicitly load CartItems
                .FirstOrDefault(c => c.UserProfileId == id);

            AllGamesVM model = new AllGamesVM();
            model.AllGames = new List<Game>();
            if (currentCart.CartItems != null)
            {
                foreach (CartItem item in currentCart.CartItems)
                {
                    Game game = _appDbContext.Games.Find(item.GameId);
                    model.AllGames.Add(game);
                }
            }
            model.ProfileId = id;
            model.Ratings = _appDbContext.Rating.ToList();

            return View("CartPage", model);
        }


        public IActionResult AddGameToCart(int id=0,int profileId=0)
        {
            bool cartItemExisting = false;
           

            CartItem newCartItem = new CartItem();
            Models.Cart currentCart = _appDbContext.Carts.FirstOrDefault(c=>c.UserProfileId == profileId);

            CartItem cartItem = _appDbContext.CartItems.FirstOrDefault(x => x.GameId == id && x.CartId == currentCart.Id);

            if (cartItem != null)
            {
                cartItemExisting = true;
            }

            if (cartItemExisting == false)
            {
                newCartItem.GameId = id;
                newCartItem.CartId = currentCart.Id;
                _appDbContext.CartItems.Add(newCartItem);
                _appDbContext.SaveChanges();

                ViewData["Message"] = "The game has been added to the cart";

                AllGamesVM list = new AllGamesVM();
                list.ProfileId = profileId;
                list.AllGames = _appDbContext.Games.OrderBy(g => g.Name).ToList();
                list.Ratings = _appDbContext.Rating.ToList();
                return View("AllGames", list); 
            }

            else
            {
                ViewData["Message"] = "The game is already in the cart";

                AllGamesVM list = new AllGamesVM();
                list.ProfileId = profileId;
                list.AllGames = _appDbContext.Games.OrderBy(g => g.Name).ToList();
                list.Ratings = _appDbContext.Rating.ToList();
                return View("AllGames", list);
            }

        }



        public IActionResult RemoveGameFromCart(int id = 0, int profileId = 0)
        {

            Models.Cart currentCart = _appDbContext.Carts.FirstOrDefault(x => x.UserProfileId == profileId);
            CartItem cartItem = _appDbContext.CartItems.FirstOrDefault(c => c.GameId == id && c.CartId == currentCart.Id);
            currentCart.CartItems.Remove(cartItem);
            _appDbContext.CartItems.Remove(cartItem);

            _appDbContext.Carts.Update(currentCart);
            _appDbContext.SaveChanges();

            return RedirectToAction("CartPage", "Cart", new { id  = profileId });

        }

        private AppDbContext _appDbContext;
    }
}
