using FrontEnd_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Text;
using System.Net.Http.Json;
using System.Text;

namespace FrontEnd_MVC.Controllers
{
    public class LoueurController : Controller
    {
      
        private async Task<List<T>> GetRequest<T>(string chemin)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await(httpClient.GetAsync(chemin)))
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
        public IActionResult HomeLoueur()
        {
            return View();
        }     

        // ******************************************************** Notoriete**************************************************************************************
        [HttpGet]
        public async Task<ActionResult> AfficheNotoriete() //OK Antoine
        {
            try
            {
                return View(await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotoriete/"));
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
        [HttpGet]
        public async Task<ActionResult> AfficheNotorieteActive()//OK Antoine
        {
            try
            {
                return View(await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteActif/"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<ActionResult> AfficheNotorieteInactive()//OK Antoine
        {
            try
            {
                return View(await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteInactif/"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult CreateNotoriete()//OK Antoine
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostNotoriete([Bind("Libelle,CoefficientMultiplicateur")] Notoriete notoriete)//OK Antoine
        {
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostNotoriete/", notoriete);
            }
            return RedirectToAction(nameof(AfficheNotorieteActive));

        }
        public async Task<IActionResult> EditNotoriete(int? id)//OK Antoine
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id));         
        }
        public async Task<IActionResult> UptadeNotoriete([Bind("Idnotoriete,Libelle,CoefficientMultiplicateur,Inactif")] Notoriete notoriete)//OK Antoine
        {
            if (ModelState.IsValid)
            {
                await PutRequest("https://localhost:7204/api/Loueur/UptadeNotoriete/", notoriete);
            }
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }
        public async Task<IActionResult> deleteNotoriete(int? id)//OK Antoine
        {
            if (id == null)
            {
                return NotFound();
            }         
                return View(await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id));             
        }

        [HttpPost, ActionName("deleteNotoriete")]
        public async Task<ActionResult> removeNotoriete(int id)//OK Antoine
        {
                 await DeleteRequest("https://localhost:7204/api/Loueur/DeleteNotoriete/" + id);
                 return RedirectToAction(nameof(AfficheNotorieteActive));
        }

        // ******************************************************** PAYS ****************************************************************************************
        [HttpGet]
        public async Task<List<Pays>> GetAllPays()//Ok Antoine
        {
            try
            {
                return await GetRequest<Pays>("https://localhost:7204/api/Loueur/GetPays/");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ActionResult> AffichePays()//Ok Antoine
        {
            try
            {
                return View(await GetRequest<Pays>("https://localhost:7204/api/Loueur/GetPays/"));
            }
           catch(Exception ex)
            {
                throw ex;
            }          
        }   
        public IActionResult CreatePays()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostPays([Bind("Nom")] Pays pays)
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
        public async Task<Pays> GetPaysById(int id)
        {
            using (var httpClient = new HttpClient())
            {
                Pays pays = new Pays();
                using (var response = await httpClient.GetAsync("https://localhost:7204/api/Loueur/GetPaysByID/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Pays lst = JsonConvert.DeserializeObject<Pays>(apiResponse);
                        return lst;
                    }


                }
                return pays;
            }
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
        public async Task<IActionResult> UptadePays([Bind("Idpays,Nom")] Pays pays)
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

        // ******************************************************** Ville ****************************************************************************************
        [HttpGet]
        public async Task<IActionResult> AfficheVille()
        {
            var lst = new List<Ville>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync("https://localhost:7204/api/Loueur/GetVille/")))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<List<Ville>>(apiResponse);
                }
            }
            
            foreach (var item in lst)
            {
                item.IdpaysNavigation = await GetPaysById(item.Idpays);
                
            }
            return View(lst);
        }
        public async Task<IEnumerable<SelectListItem>> GetAllPaysInList()
        {
            IEnumerable<SelectListItem> lst = null;

            using (var httpClient = new HttpClient())
            {
                using (var response = await(httpClient.GetAsync("https://localhost:7204/api/Loueur/GetAllPaysInList/")))
                {

                    string apiResponse = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<IEnumerable<SelectListItem>>(apiResponse);
                    return lst;
                }
            }

            return lst;
        }          
        public async Task<IActionResult> CreateVille()
        {
           
           Ville ville = new Ville();
           ville.ListPays = await GetAllPaysInList();

            return View(ville);
        }
        [HttpPost]
        public async Task<IActionResult> PostVille([Bind("Idpays,Nom")] Ville ville)
        {



            ville.IdpaysNavigation = await GetPaysById(ville.Idpays);
            ModelState.Remove("IdpaysNavigation");
            ModelState.Remove("ListPays");
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {

                    using (var response = await httpClient.PostAsJsonAsync("https://localhost:7204/api/Loueur/PostVille/", ville))
                    {


                        if (response.IsSuccessStatusCode)
                        {

                            return RedirectToAction(nameof(AfficheVille));

                        }


                    }

                }
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            
            return View(ville);
        }
        public async Task<IActionResult> EditVille(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync("https://localhost:7204/api/Loueur/GetVilleByID/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Ville lst = JsonConvert.DeserializeObject<Ville>(apiResponse);
                        lst.IdpaysNavigation = await GetPaysById(lst.Idpays);
                        return View(lst);
                    }


                }

            }
            return RedirectToAction(nameof(AfficheVille));
        }
        public async Task<IActionResult> UptadeVille([Bind("Idville, Idpays, Nom")] Ville ville)
        {

            using (var httpClient = new HttpClient())
            {
                ville.IdpaysNavigation = await GetPaysById(ville.Idpays);
                ModelState.Remove("IdpaysNavigation");
                ModelState.Remove("ListPays");
                using (var response = await httpClient.PutAsJsonAsync("https://localhost:7204/api/Loueur/UptadeVille/", ville))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction(nameof(AfficheVille));

                    }


                }
                var errors = ModelState.Values.SelectMany(v => v.Errors);


            }
            return RedirectToAction(nameof(AfficheVille));
        }
        public async Task<IActionResult> deleteVille(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var httpClient = new HttpClient())
            {
                
                using (var response = await httpClient.GetAsync("https://localhost:7204/api/Loueur/GetVilleByID/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Ville lst = JsonConvert.DeserializeObject<Ville>(apiResponse);
                        return View(lst);
                    }


                }

            }
            return RedirectToAction(nameof(AfficheVille));
        }
        [HttpPost, ActionName("deleteVille")]
        public async Task<ActionResult> removeVille(int id)
        {


            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:7204/api/Loueur/DeleteVille/" + id))
                {


                    if (response.IsSuccessStatusCode)
                    {

                        return RedirectToAction(nameof(AfficheVille));
                    }


                }

            }
            return RedirectToAction(nameof(AfficheVille));
        }
    }
}
