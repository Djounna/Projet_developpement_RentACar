using FrontEnd_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEnd_MVC.Controllers
{
    public class PaysController : Controller
    {
        public async Task<IActionResult> AffichePays()
        {
            var lst = new List<Pays>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await(httpClient.GetAsync("https://localhost:7204/api/Loueur/GetPays/")))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<List<Pays>>(apiResponse);
                }
            }

            return View(lst);
        }
    }
}
