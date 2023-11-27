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

        public IActionResult DetailsForFriendsWishList(int id = 0, int profileId = 0, int friendsId =0)
        {
            DetailsForFriendsWishListVM model = new DetailsForFriendsWishListVM();
            UserProfile currentProfile = _appDbContext.Profiles.Find(profileId);
            model.ProfileId = profileId;
            model.ActiveGame = _appDbContext.Games.Find(id);
            model.CurrentUserPreference = _appDbContext.ProfilePreferencesList.FirstOrDefault(p => p.Id == currentProfile.CurrentPrefId);
            model.GameRecommendations = new List<Game>();
            model.FriendId = friendsId;

            // Get all games and ratings
            List<Game> AllGames = _appDbContext.Games.ToList();
            List<Rating> AllRatings = _appDbContext.Rating.ToList();

            // Calculate average ratings for all games
            var averageRatings = AllRatings.GroupBy(r => r.GameId).Select(group => new
            {
                GameId = group.Key,
                AverageRatingValue = group.Average(r => r.RatingValue)
            }).ToList(); // Materialize the query into a list

            // Use a HashSet to track unique GameIds
            HashSet<int> uniqueGameIds = new HashSet<int>();

            // Filter games based on user preferences and add them to recommendations
            foreach (var game in AllGames)
            {
                if (model.GameRecommendations.Count < 8 &&
                    (game.Category == model.CurrentUserPreference.FavCategory || game.Platform == model.CurrentUserPreference.FavPlatform) &&
                    uniqueGameIds.Add(game.Id))
                {
                    model.GameRecommendations.Add(game);
                }
            }

            // If there are not enough recommendations, add more games
            if (model.GameRecommendations.Count < 8)
            {
                foreach (var game in AllGames)
                {
                    if (model.GameRecommendations.Count < 8 && uniqueGameIds.Add(game.Id))
                    {
                        model.GameRecommendations.Add(game);
                    }
                }
            }

            // Sort the recommendations by average rating in descending order
            model.GameRecommendations = model.GameRecommendations
                .OrderByDescending(gr => averageRatings.FirstOrDefault(ar => ar.GameId == gr.Id)?.AverageRatingValue)
                .ToList();

            return View("DetailsForFriendsWishList", model);
        }

        public IActionResult DetailsForWishList(int id = 0, int profileId = 0)
        {
            GameDetailsVM model = new GameDetailsVM();
            UserProfile currentProfile = _appDbContext.Profiles.Find(profileId);
            model.ProfileId = profileId;
            model.ActiveGame = _appDbContext.Games.Find(id);
            model.CurrentUserPreference = _appDbContext.ProfilePreferencesList.FirstOrDefault(p => p.Id == currentProfile.CurrentPrefId);
            model.GameRecommendations = new List<Game>();
         

            // Get all games and ratings
            List<Game> AllGames = _appDbContext.Games.ToList();
            List<Rating> AllRatings = _appDbContext.Rating.ToList();

            // Calculate average ratings for all games
            var averageRatings = AllRatings.GroupBy(r => r.GameId).Select(group => new
            {
                GameId = group.Key,
                AverageRatingValue = group.Average(r => r.RatingValue)
            }).ToList(); // Materialize the query into a list

            // Use a HashSet to track unique GameIds
            HashSet<int> uniqueGameIds = new HashSet<int>();

            // Filter games based on user preferences and add them to recommendations
            foreach (var game in AllGames)
            {
                if (model.GameRecommendations.Count < 8 &&
                    (game.Category == model.CurrentUserPreference.FavCategory || game.Platform == model.CurrentUserPreference.FavPlatform) &&
                    uniqueGameIds.Add(game.Id))
                {
                    model.GameRecommendations.Add(game);
                }
            }

            // If there are not enough recommendations, add more games
            if (model.GameRecommendations.Count < 8)
            {
                foreach (var game in AllGames)
                {
                    if (model.GameRecommendations.Count < 8 && uniqueGameIds.Add(game.Id))
                    {
                        model.GameRecommendations.Add(game);
                    }
                }
            }

            // Sort the recommendations by average rating in descending order
            model.GameRecommendations = model.GameRecommendations
                .OrderByDescending(gr => averageRatings.FirstOrDefault(ar => ar.GameId == gr.Id)?.AverageRatingValue)
                .ToList();

            return View("Details", model);
        }

        public IActionResult Details(int id = 0, int profileId = 0)
        {
            GameDetailsVM model = new GameDetailsVM();
            UserProfile currentProfile = _appDbContext.Profiles.Find(profileId);
            model.ProfileId = profileId;
            model.ActiveGame = _appDbContext.Games.Find(id);
            model.CurrentUserPreference = _appDbContext.ProfilePreferencesList.FirstOrDefault(p => p.Id == currentProfile.CurrentPrefId);
            model.GameRecommendations = new List<Game>();

            // Get all games and ratings
            List<Game> AllGames = _appDbContext.Games.ToList();
            List<Rating> AllRatings = _appDbContext.Rating.ToList();

            // Calculate average ratings for all games
            var averageRatings = AllRatings.GroupBy(r => r.GameId).Select(group => new
            {
                GameId = group.Key,
                AverageRatingValue = group.Average(r => r.RatingValue)
            }).ToList(); // Materialize the query into a list

            // Use a HashSet to track unique GameIds
            HashSet<int> uniqueGameIds = new HashSet<int>();

            // Filter games based on user preferences and add them to recommendations
            foreach (var game in AllGames)
            {
                if (model.GameRecommendations.Count < 8 &&
                    (game.Category == model.CurrentUserPreference.FavCategory || game.Platform == model.CurrentUserPreference.FavPlatform) &&
                    uniqueGameIds.Add(game.Id))
                {
                    model.GameRecommendations.Add(game);
                }
            }

            // If there are not enough recommendations, add more games
            if (model.GameRecommendations.Count < 8)
            {
                foreach (var game in AllGames)
                {
                    if (model.GameRecommendations.Count < 8 && uniqueGameIds.Add(game.Id))
                    {
                        model.GameRecommendations.Add(game);
                    }
                }
            }

            // Sort the recommendations by average rating in descending order
            model.GameRecommendations = model.GameRecommendations
                .OrderByDescending(gr => averageRatings.FirstOrDefault(ar => ar.GameId == gr.Id)?.AverageRatingValue)
                .ToList();

            return View("Details", model);
        }




        [HttpGet]
        public IActionResult AddRatings(int id = 0, int profileId = 0)
        {
            RatingVM model = new RatingVM();
            model.ProfileId = profileId;
            model.NewRating = new Rating();
            model.GameId = id;
            model.NewRating.UserProfileId = profileId;
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
                Boolean ratingExists = false;
                int existingRatingId = 0;
                if (model.RatingList != null)
                {

                    foreach (Rating r in model.RatingList)
                    {
                        if (r.GameId == model.GameId && r.UserProfileId == model.ProfileId)
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

        [HttpGet]
        public IActionResult AddReview(int id = 0, int profileId = 0)
        {
            ReviewVM model = new ReviewVM();
            model.ProfileId = profileId;
            model.NewReview = new Review();
            model.NewReview.ReviewBy = _appDbContext.Profiles.Find(profileId).DisplayName;
            model.GameId = id;
            model.NewReview.UserProfileId = profileId;
            model.NewReview.GameId = id;
            return View(model);

        }

        [HttpPost] // Handle POST requests
        public IActionResult AddReview(ReviewVM model)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Review.Add(model.NewReview);
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Review has been added successfully";
                return RedirectToAction("Index", "Games", new { id = model.ProfileId });
            }

            else
            {

                ViewData["Message"] = "";
                return View(model);
            }
        }

        public IActionResult GameReviews(int id = 0, int profileId = 0)
        {
            ReviewListVM list = new ReviewListVM();
            list.ProfileId = profileId;
            list.GameId = id;
            list.AllReviews = _appDbContext.Review.ToList();
            return View(list);
        }

        public IActionResult AddGameToWishList(int id = 0, int profileId = 0)
        {
            WishList alreadyExistingListItem = _appDbContext.WishlistItems.FirstOrDefault(w=>w.GameId==id&&w.UserProfileId == profileId);
            if (alreadyExistingListItem==null)
            {

                WishList model = new WishList();
                model.UserProfileId = profileId;
                model.GameId = id;
                _appDbContext.WishlistItems.Add(model);
                _appDbContext.SaveChanges();
                ViewData["Message"] = "Game has been added to wishlist";

                AllGamesVM list = new AllGamesVM();
                list.ProfileId = profileId;
                list.AllGames = _appDbContext.Games.OrderBy(g => g.Name).ToList();
                list.Ratings = _appDbContext.Rating.ToList();
                return View("AllGames", list);
            }

            else
            {
                ViewData["Message"] = "Game is already on the wishlist";

                AllGamesVM list = new AllGamesVM();
                list.ProfileId = profileId;
                list.AllGames = _appDbContext.Games.OrderBy(g => g.Name).ToList();
                list.Ratings = _appDbContext.Rating.ToList();
                return View("AllGames", list);
            }
           
        }

        public IActionResult EventRegister(int id = 0)
        {
            EventListVM model = new EventListVM();

            model.ProfileId = id;
            model.Events = _appDbContext.Events.ToList();

            return View(model);
        }

        public IActionResult Register(int id = 0, int profileId = 0)
        {
            List<EventParticipation> allParticipations = new List<EventParticipation>();
            allParticipations = _appDbContext.AllParticipations.ToList();

            bool alreadyParticipating = false;
            foreach (EventParticipation item in allParticipations)
            {
                if (item.UserProfileId == profileId && item.EventId == id)
                {
                    ViewData["Message"] = "You are already registered for this event";
                    alreadyParticipating = true;
                    break;
                }
            }

            if (alreadyParticipating == false)
            {
                EventParticipation newParticipation = new EventParticipation();
                newParticipation.EventId = id;
                newParticipation.UserProfileId = profileId;
                _appDbContext.AllParticipations.Add(newParticipation);
                _appDbContext.SaveChanges();
                Event currentEvent = new Event();
                currentEvent = _appDbContext.Events.Find(id);
                ViewData["Message"] = "You have been registered for the " + currentEvent.Name + " Event";
                EventListVM model = new EventListVM();
                model.ProfileId = profileId;
                model.Events = _appDbContext.Events.ToList();
                return View("EventRegister", model);
            }

            else
            {
                ViewData["Message"] = "You are already registered for this event";
                EventListVM model = new EventListVM();
                model.ProfileId = profileId;
                model.Events = _appDbContext.Events.ToList();
                return View("EventRegister", model);

            }

        }

        public IActionResult Unregister(int id = 0, int profileId = 0)
        {
            List<EventParticipation> allParticipations = new List<EventParticipation>();
            allParticipations = _appDbContext.AllParticipations.ToList();
            int currentEventParticipationId = 0;

            bool alreadyParticipating = false;
            foreach (EventParticipation item in allParticipations)
            {
                if (item.UserProfileId == profileId && item.EventId == id)
                {
                    ViewData["Message"] = "You are already registered for this event";
                    alreadyParticipating = true;
                    currentEventParticipationId = item.Id;
                    break;
                }
            }

            if (alreadyParticipating == false)
            {
                ViewData["Message"] = "You are not registered for this event";
                EventListVM model = new EventListVM();
                model.ProfileId = profileId;
                model.Events = _appDbContext.Events.ToList();
                return View("EventRegister", model);

            }

            else
            {
                EventParticipation newParticipation = new EventParticipation();
                newParticipation = _appDbContext.AllParticipations.Find(currentEventParticipationId);
                _appDbContext.AllParticipations.Remove(newParticipation);
                _appDbContext.SaveChanges();
                Event currentEvent = new Event();
                currentEvent = _appDbContext.Events.Find(id);
                ViewData["Message"] = "You have been Unregistered from the " + currentEvent.Name + " Event";
                EventListVM model = new EventListVM();
                model.ProfileId = profileId;
                model.Events = _appDbContext.Events.ToList();
                return View("EventRegister", model);
            }

        }


        public IActionResult Privacy()
        {
            return View();
        }


        private AppDbContext _appDbContext;
    }
}

