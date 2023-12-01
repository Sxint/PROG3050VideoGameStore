using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;
using System.Text;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace PROG3050VideoGameStore.Controllers
{
    public class Orders : Controller
    {
        public Orders(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult OrdersPage(int id=0)
        {
            OrderVM model = new OrderVM();
            model.ProfileId = id;

            ModelState.Clear();
            return View("OrdersPage",model);
        }

        [HttpPost()]
        public IActionResult OrdersPage(OrderVM model)
        {
            model.Order.OrderStatus = "Not Confirmed";
         
            if (ModelState.IsValid)
            {
                Models.Cart currentCart = _appDbContext.Carts
              .Include(c => c.CartItems) 
              .FirstOrDefault(c => c.UserProfileId == model.ProfileId);

                if (currentCart.CartItems != null)
                {
                    double total = 0;
                    model.Order.UserProfileId = (int)model.ProfileId;
                    _appDbContext.Orders.Add(model.Order);
                    _appDbContext.SaveChanges();

                  

                    foreach (CartItem item in currentCart.CartItems.ToList())
                    {
                        Game game = _appDbContext.Games.Find(item.GameId);
                        OrderItem orderItem = new OrderItem();
                        orderItem.OrderId = model.Order.Id;
                        orderItem.GameId = game.Id;
                        _appDbContext.OrderItems.Add(orderItem);
                        _appDbContext.SaveChanges();
                        total += game.Price;
                        _appDbContext.CartItems.Remove(item);
                        _appDbContext.SaveChanges();
                       
                    }

                   

                    model.Order.TotalCost = total;
           
                    _appDbContext.Orders.Update(model.Order);
                    _appDbContext.SaveChanges();

                    return RedirectToAction("OrderConfirmation", "Orders", new { orderId = model.Order.Id, profileId = model.ProfileId }); 
                }

                else
                {
                    OrderVM newModel = new OrderVM();
                    newModel.ProfileId = model.ProfileId;

                    ViewData["Message"] = "The cart is empty. Please add some items to the cart";
                    return View("OrdersPage", model);
                }
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
        }


        public IActionResult OrderConfirmation(int orderId,int profileId)
        {
            
            OrderConfirmationVM newModel = new OrderConfirmationVM();
            newModel.Order = _appDbContext.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == orderId);
            newModel.ProfileId = profileId;
            newModel.PurchasedGames = new List<Game>();

            Order currentOrder = _appDbContext.Orders.Include(o => o.OrderItems).FirstOrDefault(o=>o.Id==orderId);

            if (currentOrder.OrderItems != null)
            {
                foreach (OrderItem item in currentOrder.OrderItems.ToList())
                {
                    Game game = _appDbContext.Games.FirstOrDefault(g => g.Id == item.GameId);
                    newModel.PurchasedGames.Add(game);


                }
            }

            UserProfile profile = _appDbContext.Profiles.Find(profileId);
            newModel.Address = _appDbContext.UserAddresses.Find(profile.CurrentAddressId);

            

            return View("OrderConfirmation",newModel);
        }

        public IActionResult PurchasedGames(int id)
        {
            ListVM model = new ListVM();
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


            model.ProfileId = id;

            return View(model);

        }

        public IActionResult DownloadGame(int id = 0, int profileId = 0)
        {
            string textContent = _appDbContext.Games.Find(id).Name;

          
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter writer = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    writer.Write(textContent);
                    writer.Flush(); 
                }

                return File(memoryStream.ToArray(), "text/plain", "Game.txt");
            }

          
        }

        private AppDbContext _appDbContext;
    }
}
