using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using Newtonsoft.Json;

namespace API_RAC.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoueurController : ControllerBase
    {
        private BLNotoriete blNotoriete = new();
        private BLPays blpays = new();
        private BLVille blville = new();

        // *********************************************************************** Notoriete *************************************************************************
        [Route("GetNotoriete/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetNotoriete() //OK Antoine
        {
            return Ok(blNotoriete.SelectAllNotoriete());
        }

        [Route("GetNotorieteActif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetNotorieteActif() //OK Antoine
        {
            return Ok(blNotoriete.SelectAllNotorieteActif());
        }

        [Route("GetNotorieteInactif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetNotorieteInactif() //OK Antoine
        {
            return Ok(blNotoriete.SelectAllNotorieteInactif());
        }

        [Route("GetNotorieteByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Notoriete>> GetNotorieteByID(int id)//OK Antoine
        {
            return Ok(blNotoriete.GetNotorieteByID(id));
        }

        [Route("UptadeNotoriete/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UptadeNotoriete(Notoriete notoriete) //OK Antoine
        {
            blNotoriete.InsertOrUpdateNotoriete(notoriete);
            return Ok();
        }

        [Route("PostNotoriete/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostNotoriete(Notoriete notoriete)//OK Antoine
        {
            blNotoriete.InsertOrUpdateNotoriete(notoriete);
            return Ok();
        }

        /* // A remplacer.
        [Route("DeleteNotoriete/{id}")]
        [HttpDelete]
        public ActionResult DeleteNotoriete(int id)//OK Antoine
        {
            blNotoriete.DeleteNotoriete(id);
            return Ok();
        }
        */
         [Route("DeleteNotoriete/{id}")]
        [HttpDelete]
        public ActionResult DeleteNotoriete(int id)//OK Corentin, à valider par Antoine
        {
            blNotoriete.DeleteNotoriete(blNotoriete.GetNotorieteByID(id));
            return Ok();
        }


        // *********************************************************************** Pays *************************************************************************

        [Route("GetPays/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Pays>>> GetPays()//OK Antoine
        {
            return Ok(blpays.SelectAllPays());
        }

        [Route("GetPaysByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pays>> GetPaysByID(int id)
        {
            return Ok(blpays.SelectPaysByID(id));
        }//OK Antoine

        [Route("GetAllPaysInList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<SelectListItem> GetAllPaysInList()//OK Antoine
        {
            return blpays.SelectAllPaysInList();
        }

        [Route("UptadePays/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult UptadePays(Pays pays) //Ok Antoine
        {
            blpays.InsertOrUpdatePays(pays);
            return Ok();
        }
       
        [Route("PostPays/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
   
        public ActionResult PostPays(Pays pays)//OK Antoine
        {         
            blpays.InsertOrUpdatePays(pays);
            return Ok();         
        }

        [Route("DeletePays/{id}")]
        [HttpDelete]
        public ActionResult DeletePays(int id) //OK Antoine
        {
            blpays.DeletePays(id);
            return Ok();
        }

        // *********************************************************************** Ville *************************************************************************
        [Route("GetVille/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Ville>>> GetVille()//OK Antoine
        {
            return Ok(blville.SelectAllVille());
        }

        [Route("GetVilleByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pays>> GetVilleByID(int id)//OK Antoine
        {
            return Ok(blville.SelectVilleByID(id));
        }

        [Route("UptadeVille/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UptadePays(Ville ville)//OK Antoine
        {     
            blville.InsertOrUpdateVille(ville);
            return Ok();
        }

        [Route("PostVille/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostVille(Ville ville)//OK Antoine
        {
            blville.InsertOrUpdateVille(ville);
            return Ok();
        }

        [Route("DeleteVille/{id}")]
        [HttpDelete]
        public ActionResult DeleteVille(int id)//OK Antoine
        {      
            blville.DeleteVille(id);
            return Ok();
        }
    }
}
