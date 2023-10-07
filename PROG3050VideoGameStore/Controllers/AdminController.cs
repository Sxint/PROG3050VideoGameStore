using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG3050VideoGameStore.Models;

namespace PROG3050VideoGameStore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Panel()
        {
            return View();
        }

        [HttpGet] // GET request to display the edit game form
        public IActionResult EditGame()
        {
            // Logic to display the edit game form
            return View();
        }

        [HttpPost] // POST request to handle form submission
        public IActionResult EditGame(Game model)
        {
            // Logic to handle the form submission and update the game
            return RedirectToAction("Panel"); // Redirect to the desired action
        }

        public IActionResult ViewDetails()
        {
            // Logic to display the edit game form
            return View("../Games/GameDetails");
        }
    }
}
