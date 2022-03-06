using FrontEnd_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace FrontEnd_MVC.Controllers
{
    public class ClientController : Controller
    {
        private async Task<List<T>> GetRequest<T>(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync(chemin)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(apiResponse);
                }
            }
        }
        private async Task<T> GetRequestUnique<T>(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync(chemin)))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(apiResponse);

                }
            }
        }
        private async Task<ActionResult> PostRequest<T>(string chemin, T postObject)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync(chemin, postObject))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                }
            }
            return Ok();
        }
        private async Task<ActionResult> PutRequest<T>(string chemin, T putObject)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync(chemin, putObject))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                }
                return Ok();
            }
        }
        private async Task<ActionResult> DeleteRequest(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(chemin))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                }
            }
            return Ok();
        }
        private async Task<IEnumerable<SelectListItem>> GetEnumerableList(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync(chemin)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    IEnumerable<SelectListItem> lst = JsonConvert.DeserializeObject<IEnumerable<SelectListItem>>(apiResponse);
                    return lst;
                }
            }
        }
        public IActionResult HomeClient()
        {
            return View();
        }

        public async Task<IActionResult> CreateClient()//OK Antoine
        {
            return View();
        }
        public async Task<IActionResult> PostClient([Bind("Nom,Prenom,Mail")] Client client)//OK Antoine
        {
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Client/PostClient/", client);
                return await CheckLogin(client);
            }
            else
            return RedirectToAction(nameof(HomeClient));
        }
        public async Task<IActionResult> ClientConnection()//OK Antoine
        {
            return View();
        }
        public async Task<IActionResult> CheckLogin([Bind("Mail")] Client client)//OK Antoine
        {
            try
            { 
                return View(await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByMail/" + client.Mail));                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Ok();
        }

    }
}
