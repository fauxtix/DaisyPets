using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.Web.Blazor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
