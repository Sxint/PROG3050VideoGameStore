using Microsoft.AspNetCore.Mvc;

namespace PROG3050VideoGameStore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Panel()
        {
            return View();
        }
    }
}
