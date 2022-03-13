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
        public async Task<ActionResult<List<Notoriete>>> GetNotoriete() 
        {
            return Ok(blNotoriete.SelectAllNotoriete());
        }

        [Route("GetNotorieteActif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetNotorieteActif() 
        {
            return Ok(blNotoriete.SelectAllNotorieteActif());
        }

        [Route("GetNotorieteInactif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Notoriete>>> GetNotorieteInactif() 
        {
            return Ok(blNotoriete.SelectAllNotorieteInactif());
        }

        [Route("GetNotorieteByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Notoriete>> GetNotorieteByID(int id)
        {
            return Ok(blNotoriete.SelectNotorieteByID(id));
        }

        [Route("UpdateNotoriete/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateNotoriete(Notoriete notoriete) 
        {
            if (blNotoriete.InsertOrUpdateNotoriete(notoriete))
                return Ok();
            return BadRequest();
        }

        [Route("PostNotoriete/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult PostNotoriete(Notoriete notoriete)
        {
            if (blNotoriete.InsertOrUpdateNotoriete(notoriete))
                return Ok();
            return BadRequest();
        }
        [Route("DeleteNotoriete/{id}")]
        [HttpDelete]
        public ActionResult DeleteNotoriete(int id)
        {
            blNotoriete.DeleteNotoriete(id);
            return Ok();
        }


        // *********************************************************************** Pays *************************************************************************

        [Route("GetPays/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Pays>>> GetPays()
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
        public IEnumerable<SelectListItem> GetAllPaysInList()
        {
            return blpays.SelectAllPaysInList();
        }

        [Route("UpdatePays/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdatePays(Pays pays) 
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

        public ActionResult PostPays(Pays pays)
        {
            blpays.InsertOrUpdatePays(pays);
            return Ok();
        }

        [Route("DeletePays/{id}")]
        [HttpDelete]
        public ActionResult DeletePays(int id)
        {
            bool estLie = false;
            List<Ville> ville = blville.SelectAllVille();
            foreach (Ville v in ville)
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

     


        // *********************************************************************** Ville *************************************************************************
        [Route("GetVille/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Ville>>> GetVille()
        {
            return Ok(blville.SelectAllVille());
        }

        [Route("GetVilleByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Ville>> GetVilleByID(int id)
        {
            return Ok(blville.SelectVilleByID(id));
        }

        [Route("GetAllVilleInList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<SelectListItem> GetAllVilleInList()
        {
            return blville.SelectAllVilleInList();
        }

        [Route("GetAllVilleByPays/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<List<Ville>> GetAllVilleByPays(int id)
        {
            return Ok(blpays.SelectVilleByPays(id));
        }

        [Route("UpdateVille/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateVille(Ville ville)
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

        public ActionResult PostVille(Ville ville)
        {
            blville.InsertOrUpdateVille(ville);
            return Ok();
        }

        [Route("DeleteVille/{id}")]
        [HttpDelete]
        public ActionResult DeleteVille(int id)
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
        public async Task<ActionResult<List<Depot>>> GetDepot()
        {
            return Ok(bldepot.SelectAllDepot());
        }

        [Route("GetDepotByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Depot>> GetDepotByID(int id)
        {
            return Ok(bldepot.SelectDepotByID(id));
        }

        [Route("GetDepotActif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Depot>>> GetDepotActif() 
        {
            return Ok(bldepot.SelectAllDepotActif());
        }

        [Route("GetDepotInactif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Depot>>> GetDepotInactif() 
        {
            return Ok(bldepot.SelectAllDepotInactif());
        }

        [Route("GetDepotByIDVille/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Depot>> GetDepotByIDVille(int id) 
        {
            return Ok(bldepot.SelectDepotByIDVille(id));
        }


        [Route("UpdateDepot/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateDepot(Depot depot)
        {          
            bldepot.InsertOrUpdateDepot(depot);
            return Ok();
        }

        [Route("PostDepot/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostDepot(Depot Depot)
        {
            bldepot.InsertOrUpdateDepot(Depot);
            return Ok();
        }

        [Route("DeleteDepot/{id}")]
        [HttpDelete]
        public ActionResult DeleteDepot(int id)
        {
            bldepot.DeleteDepot(id);
            return Ok();
        }
    }
}
