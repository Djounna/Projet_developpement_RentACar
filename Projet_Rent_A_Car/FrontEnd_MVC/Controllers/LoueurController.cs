using FrontEnd_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Text;
using System.Net.Http.Json;
using System.Text;

namespace FrontEnd_MVC.Controllers
{
    public class LoueurController : Controller
    {

        public IActionResult HomeLoueur()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AfficheNotoriete()
        {
            var lst = new List<Notoriete>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync("https://localhost:7204/api/Loueur/GetNotoriete/")))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<List<Notoriete>>(apiResponse);
                }
            }

            return View(lst);
        }

        public IActionResult CreateNotoriete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostNotoriete([Bind("ReferencePrix,Nom")] Pays pays)
        {

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {

                    using (var response = await httpClient.PostAsJsonAsync("https://localhost:7204/api/Loueur/PostPays/", pays))
                    {


                        if (response.IsSuccessStatusCode)
                        {

                            return RedirectToAction(nameof(AffichePays));

                        }


                    }

                }
            }
            return View(pays);
        }

        public async Task<IActionResult> EditNotoriete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:7204/api/Loueur/GetPaysByID/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Pays lst = JsonConvert.DeserializeObject<Pays>(apiResponse);
                        return View(lst);
                    }


                }

            }
            return RedirectToAction(nameof(AffichePays));
        }

        public async Task<IActionResult> UptadeNotoriete([Bind("IDPays,ReferencePrix,Nom")] Pays pays)
        {

            using (var httpClient = new HttpClient())
            {
                Pays lst = new Pays();
                using (var response = await httpClient.PutAsJsonAsync("https://localhost:7204/api/Loueur/UptadePays/", pays))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction(nameof(AffichePays));

                    }


                }

            }
            return RedirectToAction(nameof(AffichePays));
        }

        public async Task<IActionResult> deleteNotoriete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var httpClient = new HttpClient())
            {
                Pays lst = new Pays();
                using (var response = await httpClient.GetAsync("https://localhost:7204/api/Loueur/GetPaysByID/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        lst = JsonConvert.DeserializeObject<Pays>(apiResponse);
                        return View(lst);
                    }


                }

            }
            return RedirectToAction(nameof(AffichePays));
        }

        [HttpPost, ActionName("deleteNotoriete")]
        public async Task<ActionResult> removeNotoriete(int id)
        {


            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:7204/api/Loueur/DeletePays/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction(nameof(AffichePays));
                    }


                }

            }
            return RedirectToAction(nameof(AffichePays));
        }

        [HttpGet]
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

        public IActionResult CreatePays()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostPays([Bind("ReferencePrix,Nom")] Pays pays)
        {            
            
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {

                    using (var response = await httpClient.PostAsJsonAsync("https://localhost:7204/api/Loueur/PostPays/", pays))
                    {


                        if (response.IsSuccessStatusCode)
                        {

                            return RedirectToAction(nameof(AffichePays));

                        }


                    }
                    
                }
            }
            return View(pays);
        }

        public async Task<IActionResult> EditPays(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var httpClient = new HttpClient())
            {
                
                using (var response = await httpClient.GetAsync("https://localhost:7204/api/Loueur/GetPaysByID/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Pays lst = JsonConvert.DeserializeObject<Pays>(apiResponse);
                        return View(lst);
                    }


                }

            }
            return RedirectToAction(nameof(AffichePays));
        }
       
        public async Task<IActionResult> UptadePays([Bind("IDPays,ReferencePrix,Nom")] Pays pays)
        {
            
            using (var httpClient = new HttpClient())
            {
                Pays lst = new Pays();
                using (var response = await httpClient.PutAsJsonAsync("https://localhost:7204/api/Loueur/UptadePays/", pays))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction(nameof(AffichePays));

                    }


                }

            }
            return RedirectToAction(nameof(AffichePays));
        }
        
        public async Task<IActionResult> deletePays(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var httpClient = new HttpClient())
            {
                Pays lst = new Pays();
                using (var response = await httpClient.GetAsync("https://localhost:7204/api/Loueur/GetPaysByID/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        lst = JsonConvert.DeserializeObject<Pays>(apiResponse);
                        return View(lst);
                    }


                }

            }
            return RedirectToAction(nameof(AffichePays));
        }

        [HttpPost, ActionName("deletePays")]
        public async Task<ActionResult> removePays(int id)
        {

           
            using (var httpClient = new HttpClient())
            {
                
                using (var response = await httpClient.DeleteAsync("https://localhost:7204/api/Loueur/DeletePays/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction(nameof(AffichePays));
                    }


                }

            }
            return RedirectToAction(nameof(AffichePays));
        }
        
    }
}
