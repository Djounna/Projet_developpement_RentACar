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

    // *********************************************************************** Notoriete *************************************************************************

        [Route("GetNotoriete/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetNotoriete()
        {

            BLNotoriete lstNotoriete = new BLNotoriete();
            List<Notoriete> notoriete = new List<Notoriete>();
            notoriete = lstNotoriete.GetAllNotoriete();
            return Ok(notoriete);

        }

        [Route("GetNotorieteActif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetNotorieteActif()
        {

            BLNotoriete lstNotoriete = new BLNotoriete();
            List<Notoriete> notoriete = lstNotoriete.GetAllNotorieteActif();
            return Ok(notoriete);

        }

        [Route("GetNotorieteInactif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetNotorieteInactif()
        {

            BLNotoriete lstNotoriete = new BLNotoriete();         
            List<Notoriete>  notoriete = lstNotoriete.GetAllNotorieteInactif();
            return Ok(notoriete);

        }

        [Route("GetNotorieteByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Notoriete>> GetNotorieteByID(int id)
        {

            BLNotoriete lstNotoriete = new BLNotoriete();
            Notoriete notoriete = new Notoriete();
            notoriete = lstNotoriete.GetNotorieteByID(id);
            return Ok(notoriete);

        }


        [Route("UptadeNotoriete/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UptadeNotoriete(Notoriete notoriete)
        {

            BLNotoriete blNotoriete= new BLNotoriete();
            blNotoriete.UptadeNotoriete(notoriete);
            return Ok();

        }


        [Route("PostNotoriete/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostNotoriete(Notoriete notoriete)
        {

            BLNotoriete blNotoriete = new BLNotoriete();
            blNotoriete.CreateNotoriete(notoriete);
            return Ok();


        }

        [Route("DeleteNotoriete/{id}")]
        [HttpDelete]
        public ActionResult DeleteNotoriete(int id)
        {

            BLNotoriete blNotoriete = new BLNotoriete();
            blNotoriete.DeleteNotoriete(id);
            return Ok();


        }


        // *********************************************************************** Pays *************************************************************************

        [Route("GetPays/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Pays>>> GetPays()
        {

            BLPays lstPays = new BLPays();
            List<Pays> pays = new List<Pays>();
            pays = lstPays.GetAllPays();
            return Ok(pays);

        }

        [Route("GetPaysByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pays>> GetPaysByID(int id)
        {

            BLPays lstPays = new BLPays();
            Pays pays = new Pays();
            pays = lstPays.GetPaysByID(id);
            return Ok(pays);

        }

        [Route("GetAllPaysInList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<SelectListItem> GetAllPaysInList()
        {
            BLPays lstPays = new BLPays();
            IEnumerable<SelectListItem> pays = lstPays.GetAllPaysInList();
            return pays;
        }


        [Route("UptadePays/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult UptadePays(Pays pays)
        {

            BLPays blPays = new BLPays();
            blPays.UptadePays(pays);
            return Ok();

        }
        

        [Route("PostPays/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
   
        public ActionResult PostPays(Pays pays)
        {
           
            BLPays blPays = new BLPays();
            blPays.CreatePays(pays);
            return Ok();
           

        }

        [Route("DeletePays/{id}")]
        [HttpDelete]
        public ActionResult DeletePays(int id)
        {

            BLPays blPays = new BLPays();
            blPays.DeletePays(id);
            return Ok();


        }

        // *********************************************************************** Ville *************************************************************************
        [Route("GetVille/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Ville>>> GetVille()
        {

            BLVille lstVille = new BLVille();
            List<Ville> ville = new List<Ville>();
            ville = lstVille.GetAllVille();
            return Ok(ville);

        }

        [Route("GetVilleByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pays>> GetVilleByID(int id)
        {

            BLVille lstVille = new BLVille();
            Ville ville = new Ville();
            ville = lstVille.GetVilleByID(id);
            return Ok(ville);

        }


        [Route("UptadeVille/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UptadePays(Ville ville)
        {
            BLVille lstVille = new BLVille();           
            lstVille.UptadeVille(ville);
            return Ok();

        }


        [Route("PostVille/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostVille(Ville ville)
        {

            BLVille lstVille = new BLVille();
            lstVille.CreateVille(ville);
            return Ok();


        }

        [Route("DeleteVille/{id}")]
        [HttpDelete]
        public ActionResult DeleteVille(int id)
        {

            BLVille lstVille = new BLVille();            
            lstVille.DeleteVille(id);
            return Ok();


        }
    }
}
