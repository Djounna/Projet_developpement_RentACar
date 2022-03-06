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
        //var errors = ModelState.Values.SelectMany(v => v.Errors); 
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
        public IActionResult CreatePays()//Ok Antoine
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostPays([Bind("Nom")] Pays pays)//Ok Antoine
        {
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostPays/", pays);
            }
            return RedirectToAction(nameof(AffichePays));
        }
        public async Task<IActionResult> EditPays(int? id)//Ok Antoine
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id));
        }     
        public async Task<IActionResult> UptadePays([Bind("Idpays,Nom")] Pays pays)//OK Antoine
        {
            {
                if (ModelState.IsValid)
                {
                    await PutRequest("https://localhost:7204/api/Loueur/UptadePays/", pays);
                }
                return RedirectToAction(nameof(AffichePays));
            }
        }       
        public async Task<IActionResult> deletePays(int? id)//Ok Antoine
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id));
        }
        [HttpPost, ActionName("deletePays")]
        public async Task<ActionResult> removePays(int id)//Ok Antoine
        {
            await DeleteRequest("https://localhost:7204/api/Loueur/DeletePays/" + id);
            return RedirectToAction(nameof(AffichePays));
        }

        // ******************************************************** Ville ****************************************************************************************
        [HttpGet]
        public async Task<IActionResult> AfficheVille()//Ok Antoine
        {            
            List<Ville> lst = await GetRequest<Ville>("https://localhost:7204/api/Loueur/GetVille/");       
            foreach (var item in lst)
            {
                item.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + item.Idpays);               
            }
            return View(lst);
        }    
        public async Task<IActionResult> CreateVille()//OK Antoine
        {
           Ville ville = new(); 
           ville.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
           return View(ville);
        }
        [HttpPost]
        public async Task<IActionResult> PostVille([Bind("Idpays,Nom")] Ville ville)//OK Antoine
        {
            ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
            ModelState.Remove("IdpaysNavigation");
            ModelState.Remove("ListPays");
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostVille/", ville);
            }
            return RedirectToAction(nameof(AfficheVille));         
        }
        public async Task<IActionResult> EditVille(int? id)//OK Antoine
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + id));
        }
        public async Task<IActionResult> UptadeVille([Bind("Idville, Idpays, Nom")] Ville ville)//OK Antoine
        {
             ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
             ModelState.Remove("IdpaysNavigation");
             ModelState.Remove("ListPays");
             if (ModelState.IsValid)
             {
                await PutRequest("https://localhost:7204/api/Loueur/UptadeVille/", ville);
             }
             return RedirectToAction(nameof(AfficheVille));            
        }
        public async Task<IActionResult> deleteVille(int? id)//OK Ville
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + id));
        }
        [HttpPost, ActionName("deleteVille")]
        public async Task<ActionResult> removeVille(int id)
        {
            await DeleteRequest("https://localhost:7204/api/Loueur/DeleteVille/" + id);
            return RedirectToAction(nameof(AfficheVille));
        }
    }
}
