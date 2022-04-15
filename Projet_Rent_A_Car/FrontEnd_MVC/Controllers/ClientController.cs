using FrontEnd_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Web.Mvc;
using ActionNameAttribute = Microsoft.AspNetCore.Mvc.ActionNameAttribute;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using BindAttribute = Microsoft.AspNetCore.Mvc.BindAttribute;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using SelectListItem = Microsoft.AspNetCore.Mvc.Rendering.SelectListItem;

namespace FrontEnd_MVC.Controllers
{
    public class ClientController : Controller
    {
        #region Generics
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
        private async Task<IActionResult> AlreadyExist<T>(string chemin, T getObject)  // En test sur notoriete
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PutAsJsonAsync(chemin, getObject))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                }
                return BadRequest();
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
        #endregion

        #region HomeClient
        public IActionResult HomeClient()
        {
            return View();
        }

        public IActionResult CreateClient()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostClient([Bind("Nom,Prenom,Mail")] Client client)
        {
            try
            {

                var result1 = await AlreadyExist<Client>("https://localhost:7204/api/Client/AlreadyExistClient/", client);
                if (result1 is BadRequestResult)
                {
                    CustomError oError = new CustomError(6);
                    throw oError;
                }

                if (ModelState.IsValid)
            {
                var result = await PostRequest("https://localhost:7204/api/Client/PostClient/", client);
                if (result is BadRequestResult)
                {
                    CustomError oError = new CustomError(3);
                    throw oError;
                }
                Thread.Sleep(500);
                return View("CheckLogin", client);
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
                return View("CreateClient", client);
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(ex.Message);
                ViewBag.Error = oError.ErrorMessage;
                return View("CreateClient", client);
            }


            
        }

        public IActionResult ClientConnection() 
        {
            return View();
        }


        public async Task<IActionResult> CheckLogin([Bind("Mail")] Client client)
        {
            try
            {
                if (client.Idclient == 0) 
                { 
                    Client c = await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByMail/" + client.Mail);
                    if (c == null)
                    {
                        CustomError oError = new CustomError(7);
                        throw oError;
                    }
                    return View(c);
                }
                else
                    return View(client);
            }
            catch (CustomError oError)
            {
                ViewBag.Error = oError.ErrorMessage;
                return View("ClientConnection");
            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeClient");
            }

        }
        #endregion

        #region Reservation

        [HttpGet]
        public async Task<ActionResult> AfficheReservationByClient(int Id) 
        {
            try
            {
                return View(await GetRequest<Reservation>("https://localhost:7204/api/Client/GetAllReservationByClient/"+Id));

            }
            catch (Exception ex)
            {
                CustomError oError = new CustomError(4);
                ViewBag.Error = oError.ErrorMessage;
                return View("HomeClient");
            }
        }


        [HttpPost] // Cette view est un Httppost afin de pouvoir récupérer l'IdClient via un formulaire. Ok Corentin
        public async Task<IActionResult> EffectuerReservation([Bind("Idclient")] Client client)
        {

            ViewBag.Idclient = client.Idclient; //  viewbag. Je passe l'idclient à la vue via le viewbag. Ok Corentin

            Reservation reservation = new();
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


            return View(reservation);

        }

        [HttpPost]
        public async Task<IActionResult> PostReservation(Reservation Reservation)
        {
            Reservation.DateReservation = DateTime.Now;

            // Détermination automatique du forfait sur base des dépots.
            if (Reservation.IddepotRetour == 999){
                Reservation.Idforfait = null;
             }
             else {
                Forfait f = await GetRequestUnique<Forfait>("https://localhost:7204/api/Client/GetForfaitReservation/" + Reservation.IddepotDepart + "/" + Reservation.IddepotRetour);
                Reservation.Idforfait = f.Idforfait;
            }

            Reservation.IdclientNavigation = await GetRequestUnique<Client>("https://localhost:7204/api/Client/GetClientByID/" + Reservation.Idclient);

            Reservation.IddepotDepartNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IddepotDepart);
            Reservation.IddepotDepartNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IddepotDepartNavigation.Idville);
            Reservation.IddepotDepartNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IddepotDepartNavigation.IdvilleNavigation.Idpays);

            if (Reservation.IddepotRetour != 999 && Reservation.IddepotRetour != null)
            {
                Reservation.IddepotRetourNavigation = await GetRequestUnique<Depot>("https://localhost:7204/api/Loueur/GetDepotByID/" + Reservation.IddepotRetour);
                Reservation.IddepotRetourNavigation.IdvilleNavigation = await GetRequestUnique<Ville>("https://localhost:7204/api/Loueur/GetVilleByID/" + Reservation.IddepotRetourNavigation.Idville);
                Reservation.IddepotRetourNavigation.IdvilleNavigation.IdpaysNavigation = await GetRequestUnique<Pays>("https://localhost:7204/api/Loueur/GetPaysByID/" + Reservation.IddepotRetourNavigation.IdvilleNavigation.Idpays);
            }

            if(Reservation.IddepotRetour == 999)
            {
                Reservation.IddepotRetour = null;
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

            //Récupération du CoefficientMultiplicateur
            Reservation.CoefficientMultiplicateur = (decimal)Reservation.IdvoitureNavigation.IdnotorieteNavigation.CoefficientMultiplicateur;

            ModelState.Remove("IdclientNavigation");
            ModelState.Remove("IddepotDepartNavigation");
            ModelState.Remove("IddepotRetourNavigation");
            ModelState.Remove("IdforfaitNavigation");
            ModelState.Remove("IdvoitureNavigation");
            ModelState.Remove("ListPays");
            ModelState.Remove("ListDepotDepart");
            ModelState.Remove("ListDepotRetour");
            ModelState.Remove("ListVoitureDisponible");

            if (ModelState.IsValid)
            {
                await PostRequest("https://localhost:7204/api/Client/PostReservation/", Reservation);
            }
            return RedirectToAction(nameof(HomeClient));
        }


        [HttpGet]
        public async Task<ActionResult> GetAllDepotByPays(int idPays)
        {
            var depots = await GetEnumerableList("https://localhost:7204/api/Client/GetAllDepotByPaysInList/" + idPays);
            return Json(depots);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDepotRetourByDepotDepartInList(int idDepotDepart)
        {
            var depots = await GetEnumerableList("https://localhost:7204/api/Client/GetAllDepotRetourByDepotDepartInList/" + idDepotDepart);
            return Json(depots);
        }

        [HttpGet] // Corentin Test en cours
        public async Task<ActionResult> GetAllVoitureDisponibleInList(int IdDepot, DateTime DateLocation)
        {
            string date = DateLocation.ToString("dd.MM.yyyy");
            var voits = await GetEnumerableList("https://localhost:7204/api/Client/GetAllVoitureDisponibleInList/"+IdDepot+"/"+date);
            return Json(voits);
        }


        public async Task<IActionResult> AnnulerReservation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return View(await GetRequestUnique<Reservation>("https://localhost:7204/api/Loueur/GetReservationByID/" + id));
        }


        [HttpPost, ActionName("AnnulerReservation")] 
        public async Task<IActionResult> DeleteReservation(int id) //try catch ici ?
        {
            
            await DeleteRequest("https://localhost:7204/api/Client/DeleteReservation/" + id);
            return RedirectToAction(nameof(AfficheReservationByClient));
        }



        #endregion

    }
}
