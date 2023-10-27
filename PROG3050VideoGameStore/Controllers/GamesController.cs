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
            ListVM list = new ListVM();
            list.ProfileId = id;
            list.AllGames = _appDbContext.Games.OrderBy(g => g.Name).ToList();
            return View("AllGames", list);
        }


        public IActionResult Details(int id)
        {
            Game activeGame = _appDbContext.Games.Find(id);
            return View("Details", activeGame);
        }



        public IActionResult Privacy()
        {
            return View();
        }


        private AppDbContext _appDbContext;
    }
}

