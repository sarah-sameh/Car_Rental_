using Microsoft.AspNetCore.Mvc;

namespace Car_Rental.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
