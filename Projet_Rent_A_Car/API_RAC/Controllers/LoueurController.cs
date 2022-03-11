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
        private BLDepot bldepot = new();

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
            return Ok(blNotoriete.SelectNotorieteByID(id));
        }

        [Route("UpdateNotoriete/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateNotoriete(Notoriete notoriete) //OK Antoine
        {
            if (blNotoriete.InsertOrUpdateNotoriete(notoriete))
                return Ok();
            return BadRequest();
        }

        [Route("PostNotoriete/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult PostNotoriete(Notoriete notoriete)//OK Antoine
        {
            if (blNotoriete.InsertOrUpdateNotoriete(notoriete))
                return Ok();
            return BadRequest();
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
            blNotoriete.DeleteNotoriete(id);
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
        }

        [Route("GetAllPaysInList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<SelectListItem> GetAllPaysInList()//OK Antoine
        {
            return blpays.SelectAllPaysInList();
        }

        [Route("UpdatePays/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  ActionResult UpdatePays(Pays pays) //Ok Antoine
        {
            bool estLie = false;
            List<Ville> ville = blville.SelectAllVille();
            foreach (Ville v in ville)
            {
                if (v.Idpays == pays.Idpays)
                    estLie = true;
            }

            if (estLie == false)
            {
                blpays.InsertOrUpdatePays(pays);
                return Ok();
            }
            return BadRequest();
            
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
        public ActionResult DeletePays(int id)
        {
            bool estLie=false;
            List<Ville> ville = blville.SelectAllVille();
            foreach ( Ville v in ville)
            {
                if (v.Idpays == id) 
                    estLie = true;
            }

            if (estLie == false) { 
                blpays.DeletePays(id);
                return Ok();
            }
            return BadRequest();
        }

        [Route("GetAllVilleByPays/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<List<Ville>> GetAllVilleByPays(int id)//OK Antoine
        {
            return Ok(blpays.SelectVilleByPays(id));
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

        [Route("GetAllVilleInList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<SelectListItem> GetAllVilleInList()//OK Antoine
        {
            return blville.SelectAllVilleInList();
        }

        [Route("UpdateVille/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateVille(Ville ville)//OK Antoine
        {
            bool estLie = false;
            List<Depot> depot = bldepot.SelectAllDepot();
            foreach (Depot d in depot)
            {
                if (d.Idville == ville.Idville)
                    estLie = true;
            }

            if (estLie == false)
            {
                blville.InsertOrUpdateVille(ville);
                return Ok();
            }
            return BadRequest();
            
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
            bool estLie = false;
            List<Depot> depot = bldepot.SelectAllDepot();
            foreach (Depot d in depot)
            {
                if (d.Idville == id)
                    estLie = true;
            }

            if (estLie == false)
            {
                blville.DeleteVille(id);
                return Ok();
            }
            return BadRequest();
            
        }

        // *********************************************************************** Depot *************************************************************************
        [Route("GetDepot/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Depot>>> GetDepot()//OK Antoine
        {
            return Ok(bldepot.SelectAllDepot());
        }

        [Route("GetDepotByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Pays>> GetDepotByID(int id)//OK Antoine
        {
            return Ok(bldepot.SelectDepotByID(id));
        }

        [Route("GetDepotActif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetDepotActif() //OK Antoine
        {
            return Ok(bldepot.SelectAllDepotActif());
        }

        [Route("GetDepotInactif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetDepotInactif() //OK Antoine
        {
            return Ok(bldepot.SelectAllDepotInactif());
        }

        [Route("UpdateDepot/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UptadePays(Depot Depot)//OK Antoine
        {
            bldepot.InsertOrUpdateDepot(Depot);
            return Ok();
        }

        [Route("PostDepot/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostDepot(Depot Depot)//OK Antoine
        {
            bldepot.InsertOrUpdateDepot(Depot);
            return Ok();
        }

        [Route("DeleteDepot/{id}")]
        [HttpDelete]
        public ActionResult DeleteDepot(int id)//OK Antoine
        {
            bldepot.DeleteDepot(id);
            return Ok();
        }
    }
}
