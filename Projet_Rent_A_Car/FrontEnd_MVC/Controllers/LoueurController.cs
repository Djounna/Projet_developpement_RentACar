using FrontEnd_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Text;
using System.Net.Http.Json;
using System.Text;
using System.Windows;

namespace FrontEnd_MVC.Controllers
{
    public class LoueurController : Controller
    {
        // ******************************************************** Generics ************************************************************************
        #region Generics
        private async Task<List<T>> GetRequest<T>(string chemin) // et si chemin incorrect ?
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync(chemin)))
                {
                    List<T> lst = new();
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(apiResponse);
                }
            }          
        }
        private async Task<T> GetRequestUnique<T>(string chemin)  // et si chemin incorrect ?
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
        private async Task<IActionResult> PostRequest<T>(string chemin, T postObject) //return badrequest the bestway to handle an error ?
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
            return BadRequest();
        }
        private async Task<IActionResult> PutRequest<T>(string chemin, T putObject)
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
                return BadRequest();
            }
        }//return badrequest the bestway to handle an error ?
        private async Task<IActionResult> DeleteRequest(string chemin)
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
            return BadRequest();
        } //return badrequest the bestway to handle an error ?
        private async Task<IEnumerable<SelectListItem>> GetEnumerableList(string chemin) // et si chemin incorrect ?
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync(chemin)))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<SelectListItem>>(apiResponse);                    
                }
            }
        }
        public IActionResult HomeLoueur()
        {
            return View();
        }
        
        #endregion
        // ******************************************************** Notoriete ***********************************************************************
        #region Notoriete
        [HttpGet]
        public async Task<IActionResult> AfficheNotoriete() 
        {
            try
            {
                return View(await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotoriete/"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheNotorieteActive()
        {
            try
            {
                return View(await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteActif/"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheNotorieteInactive()
        {
            try
            {
                return View(await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteInactif/"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult CreateNotoriete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PostNotoriete([Bind("Libelle,CoefficientMultiplicateur")] Notoriete notoriete) //try catch ici ?
        {
            if (ModelState.IsValid)
            {
              PostRequest("https://localhost:7204/api/Loueur/PostNotoriete/", notoriete);     // si error retourne badrequest dans le post ?                                
            }
            return RedirectToAction(nameof(AfficheNotorieteActive));

        }
        public IActionResult EditNotoriete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id));
        }
        public IActionResult UpdateNotoriete([Bind("Idnotoriete,Libelle,CoefficientMultiplicateur,Inactif")] Notoriete notoriete) //try catch ici ?
        {
            if (ModelState.IsValid)
            {
                PutRequest("https://localhost:7204/api/Loueur/UpdateNotoriete/", notoriete);        // le put retourne un badrequest si error      
            }
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }
        public IActionResult deleteNotoriete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id));
        }
        [HttpPost, ActionName("Disable")]
        public async Task<IActionResult> DesactiverNotoriete(int id) //try catch ici ?
        {
            Notoriete not = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
            not.Inactif = true;

            UpdateNotoriete(not);
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }
        [HttpPost, ActionName("Activate")]
        public async Task<IActionResult> ActiverNotoriete(int id) //try catch ici ?
        {
            Notoriete not = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
            not.Inactif = false;

            UpdateNotoriete(not);
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }
        public IActionResult Disable(int? id) //try catch ici ?
        { 
            var notoriety = GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
            return View(notoriety);
        }
        public IActionResult Activate(int? id) //try catch ici ?
        {
            var notoriety = GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
            return View(notoriety);
        }

        [HttpPost, ActionName("deleteNotoriete")]  
        public async Task<IActionResult> removeNotoriete(int id)//try catch ici ?
        {
            await DeleteRequest("https://localhost:7204/api/Loueur/DeleteNotoriete/" + id);
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }
        #endregion
        // ******************************************************** Voiture *************************************************************************
        #region Voiture
        [HttpGet]
        public async Task<IActionResult> AfficheVoiture()
        {
            try
            {
                List<Voiture> lst = await GetRequest<Voiture>("https://localhost:7204/api/Loueur/GetVoiture/");
                foreach (var item in lst)
                {
                    item.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + item.Idnotoriete);
                    item.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.Iddepot);
                    item.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotNavigation.Idville);
                }

                return View(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheVoitureActive()
        {
            try
            {
                List<Voiture> lst = await GetRequest<Voiture>("https://localhost:7204/api/Loueur/GetVoitureActif/");
                foreach (var item in lst)
                {
                    item.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + item.Idnotoriete);
                    item.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.Iddepot);
                    item.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotNavigation.Idville);
                }

                return View(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheVoitureInactive()
        {
            try
            {
                List<Voiture> lst = await GetRequest<Voiture>("https://localhost:7204/api/Loueur/GetVoitureInactif/");

                     foreach (var item in lst)
                {
                    item.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + item.Idnotoriete);
                    item.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.Iddepot);
                    item.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotNavigation.Idville);
                }

                return View(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IActionResult> CreateVoiture()
        {            
            Voiture voiture = new();
            voiture.ListNotoriete = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllNotorieteInList/");
            voiture.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/");         
            return View(voiture);
        }
        [HttpPost] 
        public async Task<IActionResult> PostVoiture([Bind("Idnotoriete,Iddepot,Immatriculation,Marque")] Voiture voiture)
        {
            voiture.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + voiture.Iddepot);
            voiture.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + voiture.Idnotoriete);
            voiture.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + voiture.IddepotNavigation.Idville);
            voiture.IddepotNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + voiture.IddepotNavigation.IdvilleNavigation.Idpays);
            ModelState.Remove("IddepotNavigation");
            ModelState.Remove("IdnotorieteNavigation");
            ModelState.Remove("ListDepot");
            ModelState.Remove("ListNotoriete");

            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostVoiture/", voiture);
            }
            return RedirectToAction(nameof(AfficheVoitureActive));

        }

        public async Task<IActionResult> EditVoiture(int? id)
        {
            Voiture voiture = new();
            voiture = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);
            voiture.ListNotoriete = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllNotorieteInList/");
            voiture.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/"); 

            if (id == null)
            {
                return NotFound();
            }
            return View(voiture);
        }

        public async Task<IActionResult> UpdateVoiture([Bind("Idvoiture,Idnotoriete,Iddepot,Immatriculation,Marque")] Voiture voiture)
        {
            voiture.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + voiture.Iddepot);
            voiture.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + voiture.Idnotoriete);
            voiture.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + voiture.IddepotNavigation.Idville);
            voiture.IddepotNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + voiture.IddepotNavigation.IdvilleNavigation.Idpays);
            ModelState.Remove("IddepotNavigation");
            ModelState.Remove("IdnotorieteNavigation");
            ModelState.Remove("ListDepot");
            ModelState.Remove("ListNotoriete");
            if (ModelState.IsValid)
            {
                await PutRequest("https://localhost:7204/api/Loueur/UpdateVoiture/", voiture);
            }
            return RedirectToAction(nameof(AfficheVoitureActive));
        }
        public async Task<IActionResult> deleteVoiture(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id));
        }
        
        [HttpPost, ActionName("DisableVoiture")]
        public async Task<IActionResult> DesactiverVoiture(int id)
        {
            Voiture voit = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);
            voit.Inactif = true;

            await UpdateVoiture(voit);
            return RedirectToAction(nameof(AfficheVoitureActive));
        }
        [HttpPost, ActionName("ActivateVoiture")]
        public async Task<IActionResult> ActiverVoiture(int id)
        {
            Voiture voit = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);
            voit.Inactif = false;

            await UpdateVoiture(voit);
            return RedirectToAction(nameof(AfficheVoitureActive));
        }

        public async Task<IActionResult> DisableVoiture(int? id)
        {
            var voiture = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);
            return View(voiture);
        }
        public async Task<IActionResult> ActivateVoiture(int? id)
        {
            var voiture = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);
            return View(voiture);
        }
    
        [HttpPost, ActionName("deleteVoiture")]
        public async Task<IActionResult> removeVoiture(int id)
        {
            await DeleteRequest("https://localhost:7204/api/Loueur/DeleteVoiture/" + id);
            return RedirectToAction(nameof(AfficheVoitureActive));
        }

        #endregion
        // ******************************************************** Pays ****************************************************************************
        #region Pays
        [HttpGet]
        public async Task<List<Pays>> GetAllPays()
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
        public async Task<IActionResult> AffichePays()
        {
            try
            {
                List<Pays> lstPays = await GetRequest<Pays>("https://localhost:7204/api/Loueur/GetPays/");
                foreach( Pays p in lstPays)
                {
                    p.Ville = await GetRequest<Ville>("https://localhost:7204/api/Loueur/GetAllVilleByPays/" + p.Idpays);
                    p.Price = await GetRequestUnique<Prix>("https://localhost:7204/api/Loueur/GetPriceByPays/" + p.Idpays);
                }
                return View(lstPays);
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
            ModelState.Remove("Price");
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostPays/", pays);
            }
            return RedirectToAction(nameof(AffichePays));
        }

        public async Task<IActionResult> EditPays(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id));
        }     
        public async Task<IActionResult> UpdatePays([Bind("Idpays,Nom")] Pays pays)
        {
            {
                if (ModelState.IsValid)
                {
                    await PutRequest("https://localhost:7204/api/Loueur/UpdatePays/", pays);
                }
                return RedirectToAction(nameof(AffichePays));
            }
        }       
        public async Task<IActionResult> deletePays(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id));
        }
        [HttpPost, ActionName("deletePays")]
        public async Task<IActionResult> removePays(int id)
        {
            var response = await DeleteRequest("https://localhost:7204/api/Loueur/DeletePays/" + id);
          
             return RedirectToAction(nameof(AffichePays));
            //return View("DeletePays",await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id));
        }
        #endregion
        // ******************************************************** Prix ****************************************************************************
        #region Prix
        [HttpGet]
        public async Task<IActionResult> AffichePrix()
        {
            List<Prix> lst = await GetRequest<Prix>("https://localhost:7204/api/Loueur/GetPrix/");
            foreach (var item in lst)
            {
                item.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + item.Idpays);               
            }
            return View(lst);
        }
        public async Task<IActionResult> CreatePrix()
        {
            Prix prix = new();
            prix.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
            return View(prix);
        }
        [HttpPost]
        public async Task<IActionResult> PostPrix(Prix prix)
        {
            prix.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + prix.Idpays);
            prix.DateDebut=DateTime.Now;
            ModelState.Remove("IdpaysNavigation");
            ModelState.Remove("ListPays");
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostPrix/", prix);
            }
            Thread.Sleep(500);
            return RedirectToAction(nameof(AffichePrix));
        }
        public async Task<IActionResult> EditPrix(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Prix>("https://localhost:7204/api/Loueur/GetPrixByID/" + id));
        }
        public async Task<IActionResult> UpdatePrix(Prix prix)
        {

            prix.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + prix.Idpays);
            ModelState.Remove("IdpaysNavigation");
            ModelState.Remove("ListPays");

            if (ModelState.IsValid)
            {
                await PutRequest("https://localhost:7204/api/Loueur/UpdatePrix/", prix);
                await PostPrix(prix);
            }

            return RedirectToAction(nameof(AffichePrix));
        }
        public async Task<IActionResult> deletePrix(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Prix prix = await GetRequestUnique<Prix>("https://localhost:7204/api/Loueur/GetPrixByID/" + id);
            prix.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + prix.Idpays);
            return View(prix);
        }
        [HttpPost, ActionName("deletePrix")]
        public async Task<IActionResult> removePrix(int id)
        {
            Prix prix = await GetRequestUnique<Prix>("https://localhost:7204/api/Loueur/GetPrixByID/" + id);
            prix.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + prix.Idpays);
            ModelState.Remove("IdpaysNavigation");
            ModelState.Remove("ListPays");

            if (ModelState.IsValid)
            {
                await PutRequest("https://localhost:7204/api/Loueur/UpdatePrix/", prix);
            }

            return RedirectToAction(nameof(AffichePrix));
        }
        #endregion
        // ******************************************************** Ville ***************************************************************************
        #region Ville
        [HttpGet]
        public async Task<IActionResult> AfficheVille()
        {            
            List<Ville> lst = await GetRequest<Ville>("https://localhost:7204/api/Loueur/GetVille/");       
            foreach (var item in lst)
            {             
                item.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + item.Idpays);   
                item.Depot.Add(await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByIDVille/" + item.Idville));
            }
            return View(lst);
        }    
        public async Task<IActionResult> CreateVille()
        {
           Ville ville = new(); 
           ville.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
           return View(ville);
        }
        [HttpPost]
        public async Task<IActionResult> PostVille([Bind("Idpays,Nom")] Ville ville)
        {
            ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
            ModelState.Remove("IdpaysNavigation");
            ModelState.Remove("ListPays");
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostVille/", ville);
            }
            Thread.Sleep(500);
            return RedirectToAction(nameof(AfficheVille));     
        }
        public async Task<IActionResult> EditVille(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + id));
        }
        public async Task<IActionResult> UpdateVille([Bind("Idville, Idpays, Nom")] Ville ville)
        {
             ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
             ModelState.Remove("IdpaysNavigation");
             ModelState.Remove("ListPays");
             if (ModelState.IsValid)
             {
                await PutRequest("https://localhost:7204/api/Loueur/UpdateVille/", ville);
             }
            
             return RedirectToAction(nameof(AfficheVille));            
        }
        public async Task<IActionResult> deleteVille(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ville ville = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + id);
            ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
            return View(ville);
        }
        [HttpPost, ActionName("deleteVille")]
        public async Task<IActionResult> removeVille(int id)
        {           
            await DeleteRequest("https://localhost:7204/api/Loueur/DeleteVille/" + id);
            return RedirectToAction(nameof(AfficheVille));
        }
        #endregion
        // ******************************************************** Dépôt ***************************************************************************
        #region Depot
        [HttpGet]
        public async Task<IActionResult> AfficheDepot()
        {
            List<Depot> lst = await GetRequest<Depot>("https://localhost:7204/api/Loueur/GetDepot/");
            foreach (var item in lst)
            {
                item.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.Idville);               
            }
            return View(lst);
        }

        [HttpGet]
        public async Task<IActionResult> AfficheDepotActive()
        {
            try
            {
                List<Depot> lst = await GetRequest<Depot>("https://localhost:7204/api/Loueur/GetDepotActif/");
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        item.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.Idville);
                        item.ForfaitIddepot1Navigation.Add(await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByIDDepot/" + item.Iddepot));
                        int c=3;
                    }
                    return View(lst);
                }
                lst = new();
                return View(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheDepotInactive()
        {
            try
            {
                List<Depot> lst = await GetRequest<Depot>("https://localhost:7204/api/Loueur/GetDepotInactif/");
                foreach (var item in lst)
                {
                    item.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.Idville);
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IActionResult> CreateDepot()
        {
            Depot Depot = new();
            Depot.ListVille = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllVilleInList/");
            return View(Depot);
        }
        [HttpPost]
        public async Task<IActionResult> PostDepot(Depot depot)
        {
            depot.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + depot.Idville);
            depot.IdvilleNavigation.IdpaysNavigation= await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + depot.IdvilleNavigation.Idpays);
            ModelState.Remove("IdvilleNavigation");
            ModelState.Remove("ListVille");
            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Loueur/PostDepot/", depot);
            }
            Thread.Sleep(500);
            return RedirectToAction(nameof(AfficheDepotActive));
        }
        public async Task<IActionResult> UpdateDepot(Depot depot)
        {
            depot.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + depot.Idville);
            depot.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + depot.IdvilleNavigation.Idpays);
            ModelState.Remove("IdvilleNavigation");
            ModelState.Remove("ListVille");
            if (ModelState.IsValid)
                {
                    await PutRequest("https://localhost:7204/api/Loueur/UpdateDepot/", depot);
                }
                return RedirectToAction(nameof(AfficheDepotActive));
            
        }
        public async Task<IActionResult> deleteDepot(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
            dep.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + dep.Idville);
            return View(dep);
        }
        [HttpPost, ActionName("deleteDepot")]
        public async Task<IActionResult> removeDepot(int id)
        {
            await DeleteRequest("https://localhost:7204/api/Loueur/DeleteDepot/" + id);
            return RedirectToAction(nameof(AfficheDepot));
        }

        [HttpPost, ActionName("DisableDepot")]
        public async Task<IActionResult> DesactiverDepot(int id)
        {
            Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
            dep.Inactif = true;

            await UpdateDepot(dep);
            return RedirectToAction(nameof(AfficheDepotActive));
        }

        [HttpPost, ActionName("ActivateDepot")]
        public async Task<IActionResult> ActiverDepot(int id)
        {
            Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
            dep.Inactif = false;

            await UpdateDepot(dep);
            return RedirectToAction(nameof(AfficheDepotActive));
        }
        public async Task<IActionResult> DisableDepot(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
            dep.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + dep.Idville);
            return View(dep);
        }
        public async Task<IActionResult> ActivateDepot(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
            dep.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + dep.Idville);
            return View(dep);
        }
        #endregion
        // ******************************************************** Forfait *************************************************************************
        #region Forfait
        [HttpGet]
        public async Task<IActionResult> AfficheForfait()
        {
            List<Forfait> lst = await GetRequest<Forfait>("https://localhost:7204/api/Loueur/GetForfait/");
            if (lst != null)
            {
                foreach (var item in lst)
                {
                    item.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.Iddepot1);
                    item.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.Iddepot2);

                    item.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.Iddepot1Navigation.Idville);
                    item.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.Iddepot2Navigation.Idville);
                }
                
            }
            else
            {
                lst = new();
              
            }
            return View(lst);
        }
        public async Task<IActionResult> CreateForfait()
        {
            Forfait forfait = new();
            

            forfait.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/");

          
            return View(forfait);
        }
        [HttpPost]
        public async Task<IActionResult> PostForfait(Forfait forfait)
        {
            forfait.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot1);
            forfait.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot2);
            forfait.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot1Navigation.Idville);
            forfait.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot2Navigation.Idville);
            forfait.Iddepot1Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + forfait.Iddepot1Navigation.IdvilleNavigation.Idpays);
            forfait.Iddepot2Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + forfait.Iddepot2Navigation.IdvilleNavigation.Idpays);
            forfait.DateDebut=DateTime.Now;
            ModelState.Remove("Iddepot1Navigation");
            ModelState.Remove("Iddepot2Navigation");
            ModelState.Remove("Reservation");
            ModelState.Remove("ListDepot");
            if (ModelState.IsValid)
            {
                
                await PostRequest("https://localhost:7204/api/Loueur/PostForfait/", forfait);
            }
           
            Thread.Sleep(500);
            return RedirectToAction(nameof(AfficheForfait));
        }
        public async Task<IActionResult> EditForfait(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Forfait forfait = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + id);
            forfait.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot1);
            forfait.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot2);
            forfait.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot1Navigation.Idville);
            forfait.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot2Navigation.Idville);

            return View(forfait);
        }
        public async Task<IActionResult> DeleteAndPostForfait(Forfait forfait)
        {
            removeForfait(forfait.Idforfait);
            forfait.Idforfait = 0;
            PostForfait(forfait);
            Thread.Sleep(2000);
            return RedirectToAction(nameof(AfficheForfait));
        }
        public async Task<IActionResult> UpdateForfait(Forfait forfait)
        {

            ModelState.Remove("Iddepot1Navigation");
            ModelState.Remove("Iddepot2Navigation");
            ModelState.Remove("Reservation");
            if (ModelState.IsValid)
            {
                await PutRequest("https://localhost:7204/api/Loueur/UpdateForfait/", forfait);
            }
            return RedirectToAction(nameof(AfficheForfait));

        }
        public async Task<IActionResult> deleteForfait(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Forfait forfait = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + id);
            forfait.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot1);
            forfait.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot2);
            forfait.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot1Navigation.Idville);
            forfait.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot2Navigation.Idville);
            return View(forfait);
        }
        [HttpPost, ActionName("deleteForfait")]
        public async Task<IActionResult> removeForfait(int id)
        {
            Forfait forfait = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + id);
            forfait.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot1);
            forfait.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot2);
            forfait.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot1Navigation.Idville);
            forfait.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot2Navigation.Idville);
            forfait.Iddepot1Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + forfait.Iddepot1Navigation.IdvilleNavigation.Idpays);
            forfait.Iddepot2Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + forfait.Iddepot1Navigation.IdvilleNavigation.Idpays);
            forfait.DateFin = DateTime.Now;

            await UpdateForfait(forfait);
            return RedirectToAction(nameof(AfficheForfait));
        }
        #endregion
        // ******************************************************** Réservation *********************************************************************
        #region Reservation
        [HttpGet]
        public async Task<IActionResult> AfficheReservation() // ok
        {
            try
            {
                List<Reservation> lst = await GetRequest<Reservation>("https://localhost:7204/api/Loueur/GetReservation/");
                foreach (var item in lst)
                {
                    item.IdclientNavigation = await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByID/" + item.Idclient);
                    item.IddepotDepartNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.IddepotDepart);
                    item.IddepotRetourNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.IddepotRetour);
                    item.IdvoitureNavigation = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + item.Idvoiture);
                    item.IddepotDepartNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotDepartNavigation.Idville);
                    if (item.IddepotRetourNavigation != null)
                    item.IddepotRetourNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotRetourNavigation.Idville);
                }
                return View(lst);              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheReservationNotYetStarted() 
        {
            try
            {
                List<Reservation> lst = await GetRequest<Reservation>("https://localhost:7204/api/Loueur/GetAllReservationNotYetStarted/");
                foreach (var item in lst)
                {
                    item.IdclientNavigation = await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByID/" + item.Idclient);
                    item.IddepotDepartNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.IddepotDepart);
                    item.IddepotRetourNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.IddepotRetour);
                    item.IdvoitureNavigation = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + item.Idvoiture);
                    item.IddepotDepartNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotDepartNavigation.Idville);
                    item.IddepotRetourNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotRetourNavigation.Idville);
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheReservationCloturees() 
        {
            try
            {
                List<Reservation> lst = await GetRequest<Reservation>("https://localhost:7204/api/Loueur/GetAllReservationCloturees/");
                foreach (var item in lst)
                {
                    item.IdclientNavigation = await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByID/" + item.Idclient);
                    item.IddepotDepartNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.IddepotDepart);
                    item.IddepotRetourNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.IddepotRetour);
                    item.IdvoitureNavigation = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + item.Idvoiture);
                    item.IddepotDepartNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotDepartNavigation.Idville);
                    if (item.IddepotRetourNavigation != null)
                        item.IddepotRetourNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotRetourNavigation.Idville);
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }
        [HttpGet]
        public async Task<IActionResult> AfficheLocationEnCours() 
        {
            try
            {
                List<Reservation> lst = await GetRequest<Reservation>("https://localhost:7204/api/Loueur/GetAllLocationEnCours/");
                foreach (var item in lst)
                {
                    item.IdclientNavigation = await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByID/" + item.Idclient);
                    item.IddepotDepartNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.IddepotDepart);
                    item.IddepotRetourNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.IddepotRetour);
                    item.IdvoitureNavigation = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + item.Idvoiture);
                    item.IddepotDepartNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotDepartNavigation.Idville);
                    if (item.IddepotRetourNavigation != null)
                        item.IddepotRetourNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotRetourNavigation.Idville);
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<IActionResult> DemarrerLocation(int? id)
        {
            Reservation reservation = new Reservation();
            reservation = await GetRequestUnique<Reservation>("https://localhost:7204/api/Loueur/GetReservationByID/" + id);
            
            reservation.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
            reservation.ListDepotDepart = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }  };
            reservation.ListDepotRetour = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }  };
            reservation.ListVoitureDisponible = new List<SelectListItem>() {
                 new SelectListItem
                 {
                     Value = null,
                     Text = " "
                 }  };

            
            if (id == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        public async Task<IActionResult> CloturerLocation(int? id)
        {
            Reservation reservation = new Reservation();
            reservation = await GetRequestUnique<Reservation>("https://localhost:7204/api/Loueur/GetReservationByID/" + id);
            
            reservation.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
            reservation.ListDepotDepart = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }  };
            reservation.ListDepotRetour = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Value = null,
                    Text = " "
                }  };
            reservation.ListVoitureDisponible = new List<SelectListItem>() {
                 new SelectListItem
                 {
                     Value = null,
                     Text = " "
                 }  };

            
            reservation.DateRetourPrevue = reservation.DateRetour;
            reservation.IddepotRetourPrevu = reservation.IddepotRetour;
            reservation.IddepotRetourNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + reservation.IddepotRetour);
            reservation.IddepotRetourNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + reservation.IddepotRetourNavigation.Idville);
            
             ViewBag.DateRetourPrevue = reservation.DateRetour;  
             ViewBag.DepotRetourPrevu = reservation.IddepotRetourNavigation.IdvilleNavigation.Nom;
             

            if (id == null)
            {
                return NotFound();
            }
            return View(reservation);
        }


        public async Task<IActionResult> UpdateReservation (Reservation Reservation) // Pas utilisée.
        {

            Reservation.IdclientNavigation = await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByID/" + Reservation.Idclient);

            Reservation.IddepotDepartNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IddepotDepart);
            Reservation.IddepotDepartNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IddepotDepartNavigation.Idville);
            Reservation.IddepotDepartNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IddepotDepartNavigation.IdvilleNavigation.Idpays);

            Reservation.IddepotRetourNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IddepotRetour);
            Reservation.IddepotRetourNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IddepotRetourNavigation.Idville);
            Reservation.IddepotRetourNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IddepotRetourNavigation.IdvilleNavigation.Idpays);

            Reservation.IdvoitureNavigation = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + Reservation.Idvoiture);
            Reservation.IdvoitureNavigation.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + Reservation.IdvoitureNavigation.Idnotoriete);
            Reservation.IdvoitureNavigation.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IdvoitureNavigation.Iddepot);
            Reservation.IdvoitureNavigation.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IdvoitureNavigation.IddepotNavigation.Idville);
            Reservation.IdvoitureNavigation.IddepotNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IdvoitureNavigation.IddepotNavigation.IdvilleNavigation.Idpays);

            Reservation.IdforfaitNavigation = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + Reservation.Idforfait);
            Reservation.IdforfaitNavigation.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IdforfaitNavigation.Iddepot1);
            Reservation.IdforfaitNavigation.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IdforfaitNavigation.Iddepot2);
            Reservation.IdforfaitNavigation.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IdforfaitNavigation.Iddepot1Navigation.Idville);
            Reservation.IdforfaitNavigation.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IdforfaitNavigation.Iddepot2Navigation.Idville);
            Reservation.IdforfaitNavigation.Iddepot1Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IdforfaitNavigation.Iddepot1Navigation.IdvilleNavigation.Idpays);
            Reservation.IdforfaitNavigation.Iddepot2Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IdforfaitNavigation.Iddepot2Navigation.IdvilleNavigation.Idpays);

            ModelState.Remove("IdclientNavigation");
            ModelState.Remove("IddepotDepartNavigation");
            ModelState.Remove("IddepotRetourNavigation");
            ModelState.Remove("IdforfaitNavigation");
            ModelState.Remove("IdvoitureNavigation");
            ModelState.Remove("CoefficientMultiplicateur");
            ModelState.Remove("ListPays");
            ModelState.Remove("ListDepotDepart");
            ModelState.Remove("ListDepotRetour");
            ModelState.Remove("ListVoitureDisponible");

            if (ModelState.IsValid)
            {
                await PutRequest("https://localhost:7204/api/Loueur/UpdateReservation/", Reservation);
            }
            return RedirectToAction(nameof(AfficheReservation));
        }


        public async Task<IActionResult> StartReservation(Reservation Reservation)
        {

            Reservation.IdclientNavigation = await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByID/" + Reservation.Idclient);

            Reservation.IddepotDepartNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IddepotDepart);
            Reservation.IddepotDepartNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IddepotDepartNavigation.Idville);
            Reservation.IddepotDepartNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IddepotDepartNavigation.IdvilleNavigation.Idpays);

            if (Reservation.IddepotRetour != null)
            {
                Reservation.IddepotRetourNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IddepotRetour);
                Reservation.IddepotRetourNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IddepotRetourNavigation.Idville);
                Reservation.IddepotRetourNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IddepotRetourNavigation.IdvilleNavigation.Idpays);
            }

            Reservation.IdvoitureNavigation = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + Reservation.Idvoiture);
            Reservation.IdvoitureNavigation.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + Reservation.IdvoitureNavigation.Idnotoriete);
            Reservation.IdvoitureNavigation.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IdvoitureNavigation.Iddepot);
            Reservation.IdvoitureNavigation.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IdvoitureNavigation.IddepotNavigation.Idville);
            Reservation.IdvoitureNavigation.IddepotNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IdvoitureNavigation.IddepotNavigation.IdvilleNavigation.Idpays);

            if (Reservation.Idforfait != null)
            {
                Reservation.IdforfaitNavigation = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + Reservation.Idforfait);
                Reservation.IdforfaitNavigation.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IdforfaitNavigation.Iddepot1);
                Reservation.IdforfaitNavigation.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IdforfaitNavigation.Iddepot2);
                Reservation.IdforfaitNavigation.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IdforfaitNavigation.Iddepot1Navigation.Idville);
                Reservation.IdforfaitNavigation.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IdforfaitNavigation.Iddepot2Navigation.Idville);
                Reservation.IdforfaitNavigation.Iddepot1Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IdforfaitNavigation.Iddepot1Navigation.IdvilleNavigation.Idpays);
                Reservation.IdforfaitNavigation.Iddepot2Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IdforfaitNavigation.Iddepot2Navigation.IdvilleNavigation.Idpays);
            }
                     
                ModelState.Remove("IdclientNavigation");
                ModelState.Remove("IddepotDepartNavigation");
                ModelState.Remove("IddepotRetourNavigation");
                ModelState.Remove("IdforfaitNavigation");
                ModelState.Remove("IdvoitureNavigation");
                ModelState.Remove("CoefficientMultiplicateur");
                ModelState.Remove("ListPays");
                ModelState.Remove("ListDepotDepart");
                ModelState.Remove("ListDepotRetour");
                ModelState.Remove("ListVoitureDisponible");

                if (ModelState.IsValid)
                {
                    await PutRequest("https://localhost:7204/api/Loueur/StartReservation/", Reservation);
                }
                return RedirectToAction(nameof(AfficheReservation));
        }


        public async Task<IActionResult> CloseReservation(Reservation Reservation)
        {
            
            Reservation.DateRetour = DateTime.Now;

            // Assignation du boolén Pénalité // Ok Corentin
            if (/*Reservation.DateRetour.Date != Reservation.DateRetourPrevue.Date  ||*/ Reservation.IddepotRetour != Reservation.IddepotRetourPrevu)
            {
                Reservation.Penalite = true;
            }
            else
            {
                Reservation.Penalite = false;
            }

            // Détermination du nouveau forfait si dépot retour différent et prix du dépot > supérieur au dépot prévu .
            // OK Corentin mais il faut ajouter une condition si pas de forfait correspondant car plantage.
            if (Reservation.IddepotRetour != Reservation.IddepotRetourPrevu)
            {
                Forfait f = await GetRequestUnique<Forfait>("https://localhost:7204/api/Client/GetForfaitReservation/" + Reservation.IddepotDepart + "/" + Reservation.IddepotRetour);
                
                if (f is not null) { 

                    int newforfaitId = f.Idforfait;

                    Reservation.IdforfaitNavigation = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + Reservation.Idforfait);
               
                    if (f.Prix > Reservation.IdforfaitNavigation.Prix) // Sauvegarde du forfait le plus cher, pour calcul du prix si pénalité 
                    {
                        Reservation.Idforfait = newforfaitId;
                    }

                }
            }

            Reservation.IdclientNavigation = await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByID/" + Reservation.Idclient);

            Reservation.IddepotDepartNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IddepotDepart);
            Reservation.IddepotDepartNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IddepotDepartNavigation.Idville);
            Reservation.IddepotDepartNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IddepotDepartNavigation.IdvilleNavigation.Idpays);

            if (Reservation.IddepotRetour != null)
            {
                Reservation.IddepotRetourNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IddepotRetour);
                Reservation.IddepotRetourNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IddepotRetourNavigation.Idville);
                Reservation.IddepotRetourNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IddepotRetourNavigation.IdvilleNavigation.Idpays);
            }
            Reservation.IdvoitureNavigation = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + Reservation.Idvoiture);
            Reservation.IdvoitureNavigation.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + Reservation.IdvoitureNavigation.Idnotoriete);
            Reservation.IdvoitureNavigation.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IdvoitureNavigation.Iddepot);
            Reservation.IdvoitureNavigation.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IdvoitureNavigation.IddepotNavigation.Idville);
            Reservation.IdvoitureNavigation.IddepotNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IdvoitureNavigation.IddepotNavigation.IdvilleNavigation.Idpays);

            if(Reservation.Idforfait != null) { 
            Reservation.IdforfaitNavigation = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + Reservation.Idforfait);
            Reservation.IdforfaitNavigation.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IdforfaitNavigation.Iddepot1);
            Reservation.IdforfaitNavigation.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IdforfaitNavigation.Iddepot2);
            Reservation.IdforfaitNavigation.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IdforfaitNavigation.Iddepot1Navigation.Idville);
            Reservation.IdforfaitNavigation.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IdforfaitNavigation.Iddepot2Navigation.Idville);
            Reservation.IdforfaitNavigation.Iddepot1Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IdforfaitNavigation.Iddepot1Navigation.IdvilleNavigation.Idpays);
            Reservation.IdforfaitNavigation.Iddepot2Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IdforfaitNavigation.Iddepot2Navigation.IdvilleNavigation.Idpays);
            }

            ModelState.Remove("IdclientNavigation");
            ModelState.Remove("IddepotDepartNavigation");
            ModelState.Remove("IddepotRetourNavigation");
            ModelState.Remove("IdforfaitNavigation");
            ModelState.Remove("IdvoitureNavigation");
            ModelState.Remove("CoefficientMultiplicateur");
            ModelState.Remove("ListPays");
            ModelState.Remove("ListDepotDepart");
            ModelState.Remove("ListDepotRetour");
            ModelState.Remove("ListVoitureDisponible");

            if (ModelState.IsValid)
            {
                await PutRequest("https://localhost:7204/api/Loueur/CloseReservation/", Reservation);
            }

            return RedirectToAction(nameof(AfficheReservation));
        }
        
        /*
        public async Task<IActionResult> AfficheFacturation (int id)  // En cours Corentin
        {
            Reservation reservation = await GetRequestUnique<Reservation>("https://localhost:7204/api/Loueur/GetReservationByID/" + id);

            var result = (decimal)0;
            using (var httpClient = new HttpClient())
            {
                using (var response = await (httpClient.GetAsync("https://localhost:7204/api/Loueur/GetFactureReservation/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(apiResponse);
                }
            }
            ViewBag.Total = await 
        }
        */
        #endregion

    }
}
