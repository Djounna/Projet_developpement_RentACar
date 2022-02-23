using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using WebAPI.Models;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var lst = new List<Pays>();

            using (var httpClient= new HttpClient())
            {
                using (var response = await (httpClient.GetAsync("https://localhost:7191/api/Loueur/GetPays/")))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<List<Pays>>(apiResponse);
                }
            }

            return View(lst);
        }

        public async Task<IActionResult> Pays()
        {
            var lst = new List<Pays>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync("https://localhost:7191/api/Loueur/GetPays/")))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<List<Pays>>(apiResponse);
                }
            }

            return View(lst);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}