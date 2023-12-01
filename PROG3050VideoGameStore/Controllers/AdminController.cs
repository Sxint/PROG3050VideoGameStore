using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PROG3050VideoGameStore.Models;
using PROG3050VideoGameStore.ViewModels;

namespace PROG3050VideoGameStore.Controllers
{
    public class AdminController : Controller
    {
        public AdminController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Panel(int id = 0)
        {
            ListVM list = new ListVM();
            list.ProfileId = id;
            list.AllGames = _appDbContext.Games.OrderBy(g => g.Name).ToList();
            return View(list);
        }

        public IActionResult EventList(int id = 0)
        {
            EventListVM model = new EventListVM();

            model.ProfileId = id;
            model.Events = _appDbContext.Events.ToList();

            return View(model);
        }


        [HttpGet]
        public IActionResult AddGame(int id = 0)
        {
            AddGameVM addGameVM = new AddGameVM();
            Game newGame = new Game();
            addGameVM.Game = newGame;
            addGameVM.ProfileId = id;
            return View(addGameVM);
        }

        [HttpGet]
        public IActionResult EditGame(int id = 0, int profileId =0)
        {
            AddGameVM editGameVM = new AddGameVM();
            Game editableGame = new Game();
            editableGame = _appDbContext.Games.Find(id);
            editGameVM.Game = editableGame;
            editGameVM.ProfileId = profileId;
            return View(editGameVM);
        }

        [HttpGet]
        public IActionResult AddEvent(int id = 0)
        {
            EventVM addEventVM = new EventVM();
            Event newEvent = new Event();
            addEventVM.Event = newEvent;
            addEventVM.ProfileId = id;
            return View(addEventVM);
        }

        [HttpGet]
        public IActionResult EditEvent(int id = 0, int profileId = 0)
        {
            EventVM editEventVM = new EventVM();
            Event editableEvent = new Event();
            editableEvent = _appDbContext.Events.Find(id);
            editEventVM.Event = editableEvent;
            editEventVM.ProfileId = profileId;
            return View(editEventVM);
        }



        [HttpGet]
        public IActionResult List(int id = 0)
        {
            ListVM list = new ListVM();
            list.ProfileId = id;
            list.AllGames = _appDbContext.Games.OrderBy(g=>g.Name).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult ReviewApprovalList(int id = 0)
        {
            ReviewListVM model = new ReviewListVM();
            model.AllReviews = _appDbContext.Review.Where(r=>r.IsReviewed==false).ToList();
            model.ProfileId = id;
            model.GameId = -1;
            return View(model);
        }

      
        public IActionResult ApproveReview(int id = 0, int profileId = 0)
        {
            ReviewListVM model = new ReviewListVM();
            Review approvedReview = new Review();
            approvedReview = _appDbContext.Review.Find(id);
            approvedReview.IsReviewed = true;
            _appDbContext.Review.Update(approvedReview);
            _appDbContext.SaveChanges();
            model.AllReviews = _appDbContext.Review.Where(r => r.IsReviewed == false).ToList();
            model.ProfileId = profileId;
            model.GameId = -1;
            return View("ReviewApprovalList",model);
        }

        public IActionResult RejectReview(int id = 0, int profileId = 0)
        {
            ReviewListVM model = new ReviewListVM();
            Review rejectReview = new Review();
            rejectReview = _appDbContext.Review.Find(id);
            _appDbContext.Review.Remove(rejectReview);
            _appDbContext.SaveChanges();
            model.AllReviews = _appDbContext.Review.Where(r => r.IsReviewed == false).ToList();
            model.ProfileId = profileId;
            model.GameId = -1;
            return View("ReviewApprovalList", model);
        }

        [HttpPost] // Handle POST requests
        public IActionResult AddGame(AddGameVM model)
        {
            if (ModelState.IsValid)
            {

                _appDbContext.Games.Add(model.Game);
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Game has been added successfully";
                return RedirectToAction("Panel", new { id = model.ProfileId });
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
        }

        [HttpPost] // Handle POST requests
        public IActionResult EditGame(AddGameVM model)
        {
            if (ModelState.IsValid)
            {

                _appDbContext.Games.Update(model.Game);
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Game has been edited successfully";
                return View(model);
            }

            else
            { 
                ViewData["Message"] = "";
                return View(model);
            }
        }

        public IActionResult DeleteGame(int id = 0, int profileId = 0)
        {
            Game game = new Game();
            game = _appDbContext.Games.Find(id);
            _appDbContext.Games.Remove(game);
            _appDbContext.SaveChanges();
            return RedirectToAction("List", new { id = profileId });
        }

        public IActionResult DeleteEvent(int id = 0, int profileId = 0)
        {
            Event eventModel = new Event();
            eventModel = _appDbContext.Events.Find(id);
            _appDbContext.Events.Remove(eventModel);
            _appDbContext.SaveChanges();
            return RedirectToAction("EventList", new { id = profileId });
        }

        [HttpPost] // Handle POST requests
        public IActionResult AddEvent(EventVM model)
        {
            if (ModelState.IsValid)
            {

                _appDbContext.Events.Add(model.Event);
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Event has been added successfully";
                return RedirectToAction("Panel", new { id = model.ProfileId });
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
        }


        [HttpPost] // Handle POST requests
        public IActionResult EditEvent(EventVM model)
        {
            if (ModelState.IsValid)
            {

                _appDbContext.Events.Update(model.Event);
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Event has been edited successfully";
                return View(model);
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
        }

        public IActionResult ViewDetails()
        {
            // Logic to display the edit game form
            return View("../Games/GameDetails");
        }

        [HttpGet]
        public IActionResult OrderApprovalList(int id = 0)
        {
            OrderListVM model = new OrderListVM();
            model.Orders = _appDbContext.Orders.Include(o=>o.Profile).Where(o=>o.OrderStatus == "Not Confirmed").ToList();
            model.ProfileId = id;
            
            return View("OrderApprovalList",model);
        }

        public IActionResult ApproveOrder(int id = 0, int profileId = 0)
        {
            OrderListVM model = new OrderListVM();
            Order approvedOrder = new Order();
            approvedOrder = _appDbContext.Orders.Find(id);
            approvedOrder.OrderStatus = "Confirmed";
            _appDbContext.Orders.Update(approvedOrder);
            _appDbContext.SaveChanges();
            model.Orders = _appDbContext.Orders.Include(r=>r.Profile).Where(r => r.OrderStatus == "Not Confirmed").ToList();
            model.ProfileId = profileId;
          
            return View("OrderApprovalList", model);
        }

        public IActionResult RejectOrder(int id = 0, int profileId = 0)
        {
            OrderListVM model = new OrderListVM();
            Order rejectOrder = new Order();
            rejectOrder = _appDbContext.Orders.Include(o=>o.OrderItems).FirstOrDefault(o=>o.Id==id);
            _appDbContext.Orders.Remove(rejectOrder);
            _appDbContext.SaveChanges();


            model.Orders = _appDbContext.Orders.Include(r=>r.Profile).Where(r => r.OrderStatus == "Not Confirmed").ToList();
            model.ProfileId = profileId;
            return View("OrderApprovalList", model);
        }

        private AppDbContext _appDbContext;
    }
}
