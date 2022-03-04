using Microsoft.AspNetCore.Mvc;

namespace FrontEnd_MVC.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult HomeClient()
        {
            return View();
        }
    }
}
