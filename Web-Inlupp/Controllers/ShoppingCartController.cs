using Microsoft.AspNetCore.Mvc;

namespace Web_Inlupp.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}