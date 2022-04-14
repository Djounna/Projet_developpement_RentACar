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
        private async Task<List<T>> GetRequest<T>(string chemin) // Review ok
        {
            using (var httpClient = new HttpClient())
            {
                try {
                    using (var response = await (httpClient.GetAsync(chemin)))
                    {
                        List<T> lst = new();
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<T>>(apiResponse);
                    }
                }
                catch (Exception ex)
                {
                    throw ex; // Erreur sur le chemin
                }

            }
        }


        private async Task<T> GetRequestUnique<T>(string chemin)  // Review ok
        {
            using (var httpClient = new HttpClient())
            {
                try {
                    using (var response = await (httpClient.GetAsync(chemin)))
                    {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(apiResponse);

                    }
                }
                catch (Exception ex)
                {
                    throw ex; // Erreur sur le chemin
                }
            }
        }
        private async Task<IActionResult> PostRequest<T>(string chemin, T postObject) //review ok
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
        private async Task<IActionResult> PutRequest<T>(string chemin, T putObject) // review ok
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
        }
        private async Task<IActionResult> DeleteRequest(string chemin) // review ok
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
        }
        private async Task<IEnumerable<SelectListItem>> GetEnumerableList(string chemin) // review ok
        {
            using (var httpClient = new HttpClient())
            {
                try {
                    using (var response = await (httpClient.GetAsync(chemin)))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<IEnumerable<SelectListItem>>(apiResponse);
                    }
                }
                catch (Exception ex)
                {
                    throw ex; // Erreur sur le chemin
                }
            }
        }
        #endregion
        #region HomeLoueur
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
                List<Notoriete> lst = await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotoriete/");
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheNotorieteActive()
        {
            try
            {
                List<Notoriete> lst = await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteActif/");
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheNotorieteInactive()
        {
            try
            {
                List<Notoriete> lst = await GetRequest<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteInactif/");
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        public IActionResult CreateNotoriete()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostNotoriete([Bind("Libelle,CoefficientMultiplicateur")] Notoriete notoriete) 
        {
            try
            {
                if (notoriete.CoefficientMultiplicateur <= 0 || notoriete.CoefficientMultiplicateur > 5)
                {
                    CustomError oError = new CustomError(1);  // Exception si le coefficient multiplicateur n'est pas un décimal.
                    throw oError;
                }

                if (ModelState.IsValid)
                {
                    var result = await PostRequest("https://localhost:7204/api/Loueur/PostNotoriete/", notoriete);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    return RedirectToAction(nameof(AfficheNotorieteActive)); 
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }

            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage; 
                return View("CreateNotoriete", notoriete);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                return View("CreateNotoriete", notoriete);
            }
            

        }
        public async Task<IActionResult> EditNotoriete(int? id)
        {
            try {
                
                Notoriete not = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
                if (not == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                return View(not);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }

            
        }


        public async Task<IActionResult> UpdateNotoriete([Bind("Idnotoriete,Libelle,CoefficientMultiplicateur,Inactif")] Notoriete notoriete) 
        {
            try {

                if (notoriete.CoefficientMultiplicateur <= 0 || notoriete.CoefficientMultiplicateur > 5)
                {
                    CustomError oError = new CustomError(1);  // Exception si le coefficient multiplicateur n'est pas un décimal.
                    throw oError;
                }

                if (ModelState.IsValid)
                {
                   var result = await PutRequest("https://localhost:7204/api/Loueur/UpdateNotoriete/", notoriete);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    return RedirectToAction(nameof(AfficheNotorieteActive));
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }
            }

            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("EditNotoriete", notoriete);
            }

            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                return View("EditNotoriete", notoriete);
            }

        }



   /*     public async Task<IActionResult> deleteNotoriete(int? id)
        {
            try
            {

                Notoriete not = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
                if (not == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                return View(not);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }

        }*/
        [HttpPost, ActionName("Disable")]
        public async Task<IActionResult> DesactiverNotoriete(int id) //try catch ici ?
        {          
            try
            {

                Notoriete not = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
                if (not == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                not.Inactif = true;
                var result = await UpdateNotoriete(not);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                return RedirectToAction(nameof(AfficheNotorieteActive));
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }                      
            
        }
        [HttpPost, ActionName("Activate")]
        public async Task<IActionResult> ActiverNotoriete(int id) //try catch ici ?
        {
            try
            {
                Notoriete not = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
                if (not == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                not.Inactif = false;
                var result = await UpdateNotoriete(not);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                return RedirectToAction(nameof(AfficheNotorieteActive));
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        public async Task<IActionResult> Disable(int? id) //try catch ici ?
        {
            try
            {
                Notoriete notoriety = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
                if (notoriety == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                return View(notoriety);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
            
        }
        public async Task<IActionResult> Activate(int? id) //try catch ici ?
        {
            try
            {
                Notoriete notoriety = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + id);
                if (notoriety == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                return View(notoriety);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }

      /*  [HttpPost, ActionName("deleteNotoriete")]
        public async Task<IActionResult> removeNotoriete(int id)//try catch ici ?
        {
            await DeleteRequest("https://localhost:7204/api/Loueur/DeleteNotoriete/" + id);
            return RedirectToAction(nameof(AfficheNotorieteActive));
        }*/
        #endregion
        // ******************************************************** Voiture *************************************************************************
        #region Voiture
        [HttpGet]
        public async Task<IActionResult> AfficheVoiture()
        {
            try
            {
                List<Voiture> lst = await GetRequest<Voiture>("https://localhost:7204/api/Loueur/GetVoiture/");
                if (lst != null) { 

                    foreach (var item in lst)
                    {
                        item.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + item.Idnotoriete);
                        item.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.Iddepot);
                        item.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotNavigation.Idville);
                    }
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AfficheVoitureActive()
        {
            try
            {
                List<Voiture> lst = await GetRequest<Voiture>("https://localhost:7204/api/Loueur/GetVoitureActif/");
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        item.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + item.Idnotoriete);
                        item.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.Iddepot);
                        item.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotNavigation.Idville);
                    }
                }
                return View(lst);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> AfficheVoitureInactive()
        {
            try
            {
                List<Voiture> lst = await GetRequest<Voiture>("https://localhost:7204/api/Loueur/GetVoitureInactif/");

                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        item.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + item.Idnotoriete);
                        item.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + item.Iddepot);
                        item.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.IddepotNavigation.Idville);
                    }                 
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
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
            try
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
                    var result = await PostRequest("https://localhost:7204/api/Loueur/PostVoiture/", voiture);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    return RedirectToAction(nameof(AfficheVoitureActive));
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }

            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                voiture.ListNotoriete = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllNotorieteInList/");
                voiture.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/");
                return View("CreateVoiture", voiture);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                voiture.ListNotoriete = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllNotorieteInList/");
                voiture.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/");
                return View("CreateVoiture", voiture);
            }

        }

        public async Task<IActionResult> EditVoiture(int? id)
        {
            try
            {

                Voiture voiture = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);
    
                if (voiture == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                voiture.ListNotoriete = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllNotorieteInList/");
                voiture.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/");
                return View(voiture);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }

        }

        public async Task<IActionResult> UpdateVoiture([Bind("Idvoiture,Idnotoriete,Iddepot,Immatriculation,Marque")] Voiture voiture)
        {
            try { 
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
                        var result = await PutRequest("https://localhost:7204/api/Loueur/UpdateVoiture/", voiture);
                        if (result is BadRequestResult)
                        {
                            CustomError oError = new CustomError(3);
                            throw oError;
                        }
                        return RedirectToAction(nameof(AfficheVoitureActive));
                    }
                    else
                    {
                        CustomError oError = new CustomError(2);
                        throw oError;
                    }
                }
                catch (CustomError oError)
                {
                    ViewBag.Error = oError.ErrorMessage;
                    voiture.ListNotoriete = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllNotorieteInList/");
                    voiture.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/");
                    return View("EditVoiture", voiture);
                }

                catch (Exception ex)
                {
                    CustomError oError = new CustomError(ex.Message);
                    ViewBag.Error = oError.ErrorMessage;
                    voiture.ListNotoriete = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllNotorieteInList/");
                    voiture.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/");
                    return View("EditVoiture", voiture);
                }
            }
      /*  public async Task<IActionResult> deleteVoiture(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id));
        }*/

        [HttpPost, ActionName("DisableVoiture")]
        public async Task<IActionResult> DesactiverVoiture(int id)
        {
            try
            {
                Voiture voit = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);
                if (voit == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                voit.Inactif = true;
                var result = await UpdateVoiture(voit);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                return RedirectToAction(nameof(AfficheVoitureActive));
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        [HttpPost, ActionName("ActivateVoiture")]
        public async Task<IActionResult> ActiverVoiture(int id)
        {
            try
            {
                Voiture voit = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);
                if (voit == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                voit.Inactif = false;
                var result = await UpdateVoiture(voit);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                return RedirectToAction(nameof(AfficheVoitureActive));
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }

        public async Task<IActionResult> DisableVoiture(int? id)
        {
            try {             
                var voiture = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);
                
                if (voiture == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                voiture.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + voiture.Iddepot);
                voiture.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + voiture.IddepotNavigation.Idville);
                voiture.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + voiture.Idnotoriete);
                return View(voiture);
                }
                catch (CustomError oError)
                {
                    ViewBag.Error = oError.ErrorMessage;
                    return View("HomeLoueur");
                }
                catch (Exception ex)
                {
                    CustomError oError = new CustomError(4);
                    ViewBag.Error = oError.ErrorMessage;
                    return View("HomeLoueur");
                }
        }
        public async Task<IActionResult> ActivateVoiture(int? id)
        {
            try
            {
                var voiture = await GetRequestUnique<Voiture>("https://localhost:7204/api/Loueur/GetVoitureByID/" + id);

                if (voiture == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                voiture.IddepotNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + voiture.Iddepot);
                voiture.IddepotNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + voiture.IddepotNavigation.Idville);
                voiture.IdnotorieteNavigation = await GetRequestUnique<Notoriete>("https://localhost:7204/api/Loueur/GetNotorieteByID/" + voiture.Idnotoriete);
                return View(voiture);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }   

     /*   [HttpPost, ActionName("deleteVoiture")]
        public async Task<IActionResult> removeVoiture(int id)
        {
            await DeleteRequest("https://localhost:7204/api/Loueur/DeleteVoiture/" + id);
            return RedirectToAction(nameof(AfficheVoitureActive));
        }*/

        #endregion
        // ******************************************************** Pays ****************************************************************************
        #region Pays

        [HttpGet]
        public async Task<IActionResult> AffichePays()
        {
            try
            {
                List<Pays> lstPays = await GetRequest<Pays>("https://localhost:7204/api/Loueur/GetPays/");
                if (lstPays != null)
                {

                    foreach (Pays p in lstPays)
                    {
                        p.Ville = await GetRequest<Ville>("https://localhost:7204/api/Loueur/GetAllVilleByPays/" + p.Idpays);
                        p.Price = await GetRequestUnique<Prix>("https://localhost:7204/api/Loueur/GetPriceByPays/" + p.Idpays);
                        if (p.Price == null)
                        {
                            Prix pp = new Prix();
                            pp.PrixKm = 0;
                            p.Price = pp;
                        }
                    }
                }
                return View(lstPays);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }

        public IActionResult CreatePays()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostPays([Bind("Nom")] Pays pays)
        {
            try
            {
                ModelState.Remove("Price");
                if (ModelState.IsValid)
                {
                    var result = await PostRequest("https://localhost:7204/api/Loueur/PostPays/", pays);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    return RedirectToAction(nameof(AffichePays));
                }                
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }

            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("CreatePays", pays);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;

                return View("CreatePays", pays);
            }
        }

        public async Task<IActionResult> EditPays(int? id)
        {
            try
            {
                Pays pays = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id);
                if (pays == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                return View(pays);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
            

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePays([Bind("Idpays,Nom")] Pays pays)
        {
            try{
                ModelState.Remove("Price");
                if (ModelState.IsValid)
                {
                    var result= await PutRequest("https://localhost:7204/api/Loueur/UpdatePays/", pays);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    return RedirectToAction(nameof(AffichePays));
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("EditPays", pays);
            }

            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                return View("EditPays", pays);
            }
        
        }

        public async Task<IActionResult> deletePays(int? id)
        {
            try {
                if (id == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }       
               return View(await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id));
            }               
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }

        [HttpPost, ActionName("deletePays")]
        public async Task<IActionResult> removePays(int id)
        {
            try
            {
                var result = await DeleteRequest("https://localhost:7204/api/Loueur/DeletePays/" + id);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }

                return RedirectToAction(nameof(AffichePays));
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                Pays p = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + id);
                return View("deletePays",p);
            }
            
        }
        #endregion
        // ******************************************************** Prix ****************************************************************************
        #region Prix
        [HttpGet]
        public async Task<IActionResult> AffichePrix()
        {
            try
            {

                List<Prix> lst = await GetRequest<Prix>("https://localhost:7204/api/Loueur/GetPrix/");
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        item.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + item.Idpays);
                    }
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
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
            try
            {
                prix.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + prix.Idpays);
                prix.DateDebut = DateTime.Now;
                ModelState.Remove("IdpaysNavigation");
                ModelState.Remove("ListPays");
                if (ModelState.IsValid)
                {
                    var result = await PostRequest("https://localhost:7204/api/Loueur/PostPrix/", prix);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    Thread.Sleep(500);
                    return RedirectToAction(nameof(AffichePrix));
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }

            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                prix.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
                return View("CreatePrix", prix);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                prix.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
                return View("CreatePrix", prix);
            }
        }
        public async Task<IActionResult> EditPrix(int? id)
        {
            try
            {                
                Prix prix = await GetRequestUnique<Prix>("https://localhost:7204/api/Loueur/GetPrixByID/" + id);
                
                if (prix == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                prix.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
                return View(prix);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }

        }
        
        public async Task<IActionResult> deletePrix(int? id)
        {
            try 
            { 
                if (id == null)
                {
                CustomError oError = new CustomError(5);
                throw oError;
                }
                Prix prix = await GetRequestUnique<Prix>("https://localhost:7204/api/Loueur/GetPrixByID/" + id);
                prix.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + prix.Idpays);
                return View(prix);
            }
            catch (CustomError oError)
            {
                    ViewBag.Error = oError.ErrorMessage;
                    return View("HomeLoueur");
            }

        }
        [HttpPost, ActionName("deletePrix")]
        public async Task<IActionResult> removePrix(int id)
        {
            try 
            { 

                Prix prix = await GetRequestUnique<Prix>("https://localhost:7204/api/Loueur/GetPrixByID/" + id);
                prix.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + prix.Idpays);
                ModelState.Remove("IdpaysNavigation");
                ModelState.Remove("ListPays");

                if (ModelState.IsValid)
                {
                    var result = await PutRequest("https://localhost:7204/api/Loueur/UpdatePrix/", prix);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    return RedirectToAction(nameof(AffichePrix));
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("deletePrix", id);
            }

            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                return View("deletePrix", id);
                
            }
        }
        #endregion
        // ******************************************************** Ville ***************************************************************************
        #region Ville
        [HttpGet]
        public async Task<IActionResult> AfficheVille()
        {
            try
            {
                List<Ville> lst = await GetRequest<Ville>("https://localhost:7204/api/Loueur/GetVille/");
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        item.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + item.Idpays);
                        item.Depot.Add(await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByIDVille/" + item.Idville));
                    }                 
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
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
            try
            {
                ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
                ModelState.Remove("IdpaysNavigation");
                ModelState.Remove("ListPays");

                if (ModelState.IsValid)
                {
                    var result = await PostRequest("https://localhost:7204/api/Loueur/PostVille/", ville);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }

                    Thread.Sleep(500);
                    return RedirectToAction(nameof(AfficheVille));
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }

            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                ville.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
                return View("CreateVille", ville);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                ville.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
                return View("CreateVille", ville);
            }

        }


        public async Task<IActionResult> EditVille(int? id)
        {
            try
            {
                Ville ville = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + id);

                if (ville == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                ville.ListPays = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllPaysInList/");
                return View(ville);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }

        }
        public async Task<IActionResult> UpdateVille([Bind("Idville, Idpays, Nom")] Ville ville)
        {
            try { 
            ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
            ModelState.Remove("IdpaysNavigation");
            ModelState.Remove("ListPays");
            if (ModelState.IsValid)
            {
                var result = await PutRequest("https://localhost:7204/api/Loueur/UpdateVille/", ville);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                return RedirectToAction(nameof(AfficheVille));
            }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("EditVille", ville);
            }

            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                return View("EditVille", ville);
            }



        }
        public async Task<IActionResult> deleteVille(int? id)
        {
            try
            {
                if (id == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                Ville ville = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + id);
                ville.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + ville.Idpays);
                return View(ville);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        [HttpPost, ActionName("deleteVille")]
        public async Task<IActionResult> removeVille(int id)
        {
            try
            {
                var result = await DeleteRequest("https://localhost:7204/api/Loueur/DeleteVille/" + id);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                return RedirectToAction(nameof(AfficheVille));
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("deleteVille", id);
            }
        }
        #endregion
        // ******************************************************** Dépôt ***************************************************************************
        #region Depot
        [HttpGet]
        public async Task<IActionResult> AfficheDepot()
        {
            try
            {
                List<Depot> lst = await GetRequest<Depot>("https://localhost:7204/api/Loueur/GetDepot/");
                if (lst != null)
                {

                    foreach (var item in lst)
                    {
                        item.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.Idville);
                        item.ForfaitIddepot1Navigation.Add(await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByIDDepot/" + item.Iddepot));

                    }
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
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
                    }
                    return View(lst);
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AfficheDepotInactive()
        {
            try
            {
                List<Depot> lst = await GetRequest<Depot>("https://localhost:7204/api/Loueur/GetDepotInactif/");
                if (lst != null)
                {
                    foreach (var item in lst)
                    {
                        item.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + item.Idville);
                    }
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
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
            try
            {
                depot.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + depot.Idville);
                depot.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + depot.IdvilleNavigation.Idpays);
                ModelState.Remove("IdvilleNavigation");
                ModelState.Remove("ListVille");
                if (ModelState.IsValid)
                {
                    var result = await PostRequest("https://localhost:7204/api/Loueur/PostDepot/", depot);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    Thread.Sleep(500);
                    return RedirectToAction(nameof(AfficheDepotActive));
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }

            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                depot.ListVille = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllVilleInList/");
                return View("CreateDepot", depot);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                depot.ListVille = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllVilleInList/");
                return View("CreateDepot", depot);
            }

        }
        
      public async Task<IActionResult> UpdateDepot(Depot depot, bool Inactif)
        {
            try
            {
                depot.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + depot.Idville);
                depot.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + depot.IdvilleNavigation.Idpays);
                ModelState.Remove("IdvilleNavigation");
                ModelState.Remove("ListVille");
                if (ModelState.IsValid)
                {
                    var result = await PutRequest("https://localhost:7204/api/Loueur/UpdateDepot/", depot);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    return RedirectToAction(nameof(AfficheDepotActive));
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                depot.ListVille = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllVilleInList/");
                if (Inactif==true)
                    return View("DisableDepot", depot);
                else 
                    return View("ActivateDepot", depot);
            }

            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                depot.ListVille = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllVilleInList/");
                if (Inactif == true)
                    return View("DisableDepot", depot);
                else
                    return View("ActivateDepot", depot);
            }
        }

      //public async Task<IActionResult> deleteDepot(int? id)
      //  {
      //      try
      //      {
      //          if (id == null)
      //          {
      //              CustomError oError = new CustomError(5);
      //              throw oError;
      //          }
      //          Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
      //          dep.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + dep.Idville);
      //          return View(dep);
      //      }
      //      catch (CustomError oError)
      //      {
      //          ViewBag.Error = oError.ErrorMessage;
      //          return View("HomeLoueur");
      //      }
      //  }
        
        
        //[HttpPost, ActionName("deleteDepot")]
        //public async Task<IActionResult> removeDepot(int id)
        //{
        //    try
        //    {
        //        var result = await DeleteRequest("https://localhost:7204/api/Loueur/DeleteDepot/" + id);
        //        if (result is BadRequestResult)
        //        {
        //            CustomError oError = new CustomError(3);
        //            throw oError;
        //        }
        //        return RedirectToAction(nameof(AfficheDepot));
        //    }
        //    catch (CustomError oError)
        //    {
        //        ViewBag.Error = oError.ErrorMessage;
        //        Depot d = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
        //        d.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + d.Idville);
        //        return View("deleteDepot", d);
        //    }
        //}

        [HttpPost, ActionName("DisableDepot")]
        public async Task<IActionResult> DesactiverDepot(int id)
        {
            try
            {
                Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
                if (dep == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                dep.Inactif = true;
                var result = await UpdateDepot(dep, true);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                return RedirectToAction(nameof(AfficheDepotActive));
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }

        [HttpPost, ActionName("ActivateDepot")]
        public async Task<IActionResult> ActiverDepot(int id)

        {
            try
            {
                Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
                if (dep == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                dep.Inactif = false;
                var result = await UpdateDepot(dep, false);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                return RedirectToAction(nameof(AfficheDepotActive));
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        public async Task<IActionResult> DisableDepot(int? id)
        {
            try
            {
                Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
                if (dep == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                dep.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + dep.Idville);
                return View(dep);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        public async Task<IActionResult> ActivateDepot(int? id)
        {
            try
            {
                Depot dep = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + id);
                if (dep == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                dep.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + dep.Idville);
                return View(dep);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        #endregion
        // ******************************************************** Forfait *************************************************************************
        #region Forfait
        [HttpGet]
        public async Task<IActionResult> AfficheForfait()
        {
            try
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
                return View(lst);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
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
            try { 
            forfait.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot1);
            forfait.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot2);
            forfait.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot1Navigation.Idville);
            forfait.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot2Navigation.Idville);
            forfait.Iddepot1Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + forfait.Iddepot1Navigation.IdvilleNavigation.Idpays);
            forfait.Iddepot2Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + forfait.Iddepot2Navigation.IdvilleNavigation.Idpays);
            forfait.DateDebut = DateTime.Now;
            ModelState.Remove("Iddepot1Navigation");
            ModelState.Remove("Iddepot2Navigation");
            ModelState.Remove("Reservation");
            ModelState.Remove("ListDepot");
            if (ModelState.IsValid)
            {
                var result = await PostRequest("https://localhost:7204/api/Loueur/PostForfait/", forfait);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                    Thread.Sleep(500);
                    return RedirectToAction(nameof(AfficheForfait));
            }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }

            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                forfait.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/");
                return View("CreateForfait", forfait);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                forfait.ListDepot = await GetEnumerableList("https://localhost:7204/api/Loueur/GetAllDepotInList/");
                return View("CreateForfait", forfait);
            }


        }

        public async Task<IActionResult> EditForfait(int? id)
        {
            try
            {
                Forfait forfait = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + id);

                if (forfait == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                forfait.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot1);
                forfait.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot2);
                forfait.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot1Navigation.Idville);
                forfait.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot2Navigation.Idville);
                return View(forfait);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }

        }
        public async Task<IActionResult> DeleteAndPostForfait(Forfait forfait)
        {
            await removeForfait(forfait.Idforfait);
            forfait.Idforfait = 0;
            await PostForfait(forfait);
            Thread.Sleep(2000);
            return RedirectToAction(nameof(AfficheForfait));
        }
        public async Task<IActionResult> UpdateForfait(Forfait forfait)
        {
            try
            {

            ModelState.Remove("Iddepot1Navigation");
            ModelState.Remove("Iddepot2Navigation");
            ModelState.Remove("Reservation");
                if (ModelState.IsValid)
                {
                    var result = await PutRequest("https://localhost:7204/api/Loueur/UpdateForfait/", forfait);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    return RedirectToAction(nameof(AfficheForfait));
                }
                else
                {
                    CustomError oError = new CustomError(2);
                    throw oError;
                }
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;           
                return View("EditForfait", forfait);

            }

            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                return View("EditForfait", forfait);
            }


        }
        public async Task<IActionResult> deleteForfait(int? id)
        {
            try
            {

                if (id == null)
                {
                    CustomError oError = new CustomError(5);
                    throw oError;
                }
                Forfait forfait = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + id);
                forfait.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot1);
                forfait.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot2);
                forfait.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot1Navigation.Idville);
                forfait.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot2Navigation.Idville);
                return View(forfait);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }
        }
        [HttpPost, ActionName("deleteForfait")]
        public async Task<IActionResult> removeForfait(int id)
        {
            try
            {
                Forfait forfait = await GetRequestUnique<Forfait>("https://localhost:7204/api/Loueur/GetForfaitByID/" + id);
                forfait.Iddepot1Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot1);
                forfait.Iddepot2Navigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + forfait.Iddepot2);
                forfait.Iddepot1Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot1Navigation.Idville);
                forfait.Iddepot2Navigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + forfait.Iddepot2Navigation.Idville);
                forfait.Iddepot1Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + forfait.Iddepot1Navigation.IdvilleNavigation.Idpays);
                forfait.Iddepot2Navigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + forfait.Iddepot1Navigation.IdvilleNavigation.Idpays);
                forfait.DateFin = DateTime.Now;

                ModelState.Remove("ListDepot");
                ModelState.Remove("Iddepot1Navigation");
                ModelState.Remove("Iddepot2Navigation");
                if (ModelState.IsValid)
                {

                    var result = await UpdateForfait(forfait);
                    if (result is BadRequestResult)
                    {
                        CustomError oError = new CustomError(3);
                        throw oError;
                    }
                    return RedirectToAction(nameof(AfficheForfait));
                }
                else
                {
                    IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                    CustomError oError = new CustomError(2);
                    throw oError;
                }
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("deletePays", id);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                return View("deletePrix", id);

            }
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
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
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
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
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
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
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
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
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

        [HttpPut]
        public async Task<IActionResult> UpdateReservation(Reservation Reservation) // Pas utilisée.
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

        [HttpPut]
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

        [HttpPut]
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

            if (Reservation.Idforfait != null) {
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

        [HttpGet]
        public async Task<IActionResult> AfficheFacturation(int id)  // En cours Corentin
        {
            try
            {
                Reservation reservation = await GetRequestUnique<Reservation>("https://localhost:7204/api/Loueur/GetReservationByID/" + id);
                var result = (decimal)0;
                using (var httpClient = new HttpClient())
                {
                    using (var response = await (httpClient.GetAsync("https://localhost:7204/api/Client/GetFactureReservation/" + id)))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<decimal>(apiResponse);
                    }
                }
                reservation.PrixTotal = result;

                return View(reservation);

            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeLoueur");
            }

        }

        #endregion

    

    }
}
