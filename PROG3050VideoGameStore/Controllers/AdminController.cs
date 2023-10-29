using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IActionResult ViewDetails()
        {
            // Logic to display the edit game form
            return View("../Games/GameDetails");
        }

        private AppDbContext _appDbContext;
    }
}
