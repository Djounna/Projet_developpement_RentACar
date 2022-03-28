using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

namespace API_RAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private BLNotoriete blNotoriete = new();
        private BLPays blpays = new();
        private BLVille blville = new();
        private BLClient blclient = new();
        private BLDepot bldepot = new();
        private BLForfait blforfait = new();
        private BLReservation blReservation = new();


        [Route("GetClientById/{Id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Client>> GetClientById(int Id)// Ok 
        {
            return Ok(blclient.SelectClientById(Id));
        }



        [Route("GetClientByMail/{Mail}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Client>> GetClientByMail(string mail)//OK 
        {
            return Ok(blclient.SelectClientByMail(mail));
        }

        [Route("PostClient/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostClient(Client client)//OK 
        {
            blclient.CreateClient(client);
            return Ok();
        }


        // Insert avec une méthode ADO de la DAL. A cause de l'erreur générée par EF sur le double accès dans les dépots.
        [Route("PostReservation/")] // OK
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult PostReservation(Reservation Reservation)
        {
            blReservation.Insert(Reservation);
            return Ok();
        }

        /*
        // En attente Corentin
        [Route("DeleteReservation/{id}")] 
        [HttpDelete]
        public ActionResult DeleteReservation(int id)
        {
            blReservation.DeleteReservation(id);
            return Ok();
        }
        */



        [Route("GetAllDepotByPaysInList/{id}")] // OK, testé avec swagger
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<SelectListItem> GetAllDepotByPaysInList(int idPays)
        {
            return bldepot.SelectAllDepotByPaysInList(idPays);
        }

        /*
        [Route("GetAllDepotRetourByDepotDepartInList/{id}")] // En cours Corentin, ne fonctionne pas actuellement, voir DalDepot
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<SelectListItem> SelectAllDepotRetourByDepotDepartInList(int idDepotDepart)
        {
            return bldepot.SelectAllDepotRetourByDepotDepartInList(idDepotDepart);
        }
        */

        [HttpGet]
        [Route("GetForfaitReservation/{id1}/{id2}")]
        public ActionResult<Forfait> SelectForfaitReservation(int idDepot1, int idDepot2)
        {
            return Ok(blforfait.SelectForfaitReservation(idDepot1, idDepot2));
        }


    }
}
