using Microsoft.AspNetCore.Mvc;

namespace PROG3050VideoGameStore.Controllers
{
    public class Profile : Controller
    {
        public IActionResult profile()
        {
            return View();
        }
    }
}
