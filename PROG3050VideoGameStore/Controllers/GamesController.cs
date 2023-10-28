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
    public class GamesController : Controller
    {
        public GamesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: /<controller>/
        public IActionResult Index(int id = 0)
        {
            AllGamesVM list = new AllGamesVM();
            list.ProfileId = id;
            list.AllGames = _appDbContext.Games.OrderBy(g => g.Name).ToList();
            list.Ratings = _appDbContext.Rating.ToList();
            return View("AllGames", list);
        }


        public IActionResult Details(int id)
        {
            Game activeGame = _appDbContext.Games.Find(id);
            return View("Details", activeGame);
        }

        [HttpGet]
        public IActionResult AddRatings(int id = 0,int profileId = 0)
        {
            RatingVM model = new RatingVM();
            model.ProfileId = profileId;
            model.NewRating = new Rating();
            model.GameId = id;
            model.NewRating.ProfileId = profileId;
            model.NewRating.GameId = id;
            model.RatingList = _appDbContext.Rating.ToList();
            return View(model);

        }

        [HttpPost]
        public IActionResult AddRatings(RatingVM model)
        {

            if (ModelState.IsValid)
            {
                model.RatingList = _appDbContext.Rating.ToList();
                Boolean ratingExists=false;
                int existingRatingId = 0;
                if (model.RatingList != null)
                {

                    foreach (Rating r in model.RatingList)
                    {
                        if (r.GameId == model.GameId && r.ProfileId == model.ProfileId)
                        {
                            ratingExists = true;
                            existingRatingId = r.Id;
                            break;
                        }
                    }  
                }
               


                if (ratingExists == true)
                {
                    Rating rating = new Rating();
                    rating = _appDbContext.Rating.Find(existingRatingId);
                    rating.RatingValue = model.NewRating.RatingValue;
                    _appDbContext.Rating.Update(rating);
                    _appDbContext.SaveChanges();
                    ViewData["Message"] = "Rating has been updated successfully";
                    return View(model);

                }

                else
                {
                    _appDbContext.Rating.Add(model.NewRating);
                    _appDbContext.SaveChanges();
                    ViewData["Message"] = "Rating have been added successfully";
                    return View(model);
                }
                
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }

        }


        public IActionResult Privacy()
        {
            return View();
        }


        private AppDbContext _appDbContext;
    }
}

