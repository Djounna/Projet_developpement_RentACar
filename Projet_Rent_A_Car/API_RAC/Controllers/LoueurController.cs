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
        private BLVoiture blVoiture = new();
        private BLForfait blforfait = new();
        private BLPrix blPrix = new();

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

        // *********************************************************************** Voiture *************************************************************************
        [Route("GetVoiture/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Voiture>>> GetVoiture()
        {
            return Ok(blVoiture.SelectAllVoiture());
        }

        [Route("GetVoitureActif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Voiture>>> GetVoitureActif()
        {
            return Ok(blVoiture.SelectAllVoitureActif());
        }

        [Route("GetVoitureInactif/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Voiture>>> GetVoitureInactif()
        {
            return Ok(blVoiture.SelectAllVoitureInactif());
        }

        [Route("GetVoitureByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Voiture>> GetVoitureByID(int id)
        {
            return Ok(blVoiture.SelectVoitureByID(id));
        }

        [Route("UpdateVoiture/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateVoiture(Voiture Voiture)
        {
            blVoiture.InsertOrUpdateVoiture(Voiture);
            return Ok();     
        }

        [Route("PostVoiture/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult PostVoiture(Voiture Voiture)
        {
            blVoiture.InsertOrUpdateVoiture(Voiture);
            return Ok();
        }
        [Route("DeleteVoiture/{id}")]
        [HttpDelete]
        public ActionResult DeleteVoiture(int id)
        {
            blVoiture.DeleteVoiture(id);
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

        [Route("GetPriceByPays/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Prix GetPriceByPays(int id)
        {
            return blPrix.SelectPrixByPays(id);

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
        // *********************************************************************** Prix *************************************************************************
        [Route("GetPrix/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Prix>>> GetPrix()
        {
            return Ok(blPrix.SelectAllPrix());
        }

        [Route("GetPrixByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Prix>> GetPrixByID(int id)
        {
            return Ok(blPrix.SelectPrixByID(id));
        }

        [Route("GetAllPrixByPays/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]



        [Route("UpdatePrix/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdatePrix(Prix prix)
        {
           blPrix.UpdatePrix(prix);
           return Ok();                      

        }

        [Route("PostPrix/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostPrix(Prix Prix)
        {
            blPrix.InsertOrUpdatePrix(Prix);
            return Ok();
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

        [Route("GetAllDepotInList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<SelectListItem> GetAllDepotInList()
        {
            return bldepot.SelectAllDepotInList();
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



        // *********************************************************************** Forfait *************************************************************************
        [Route("GetForfait/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Depot>>> GetForfait()
        {
            return Ok(blforfait.SelectAllForfait());
        }

        [Route("GetForfaitByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Forfait>> GetForfaitByID(int id)
        {
            return Ok(blforfait.SelectForfaitByID(id));
        }

        [Route("GetForfaitByIDDepot/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Forfait>> GetForfaitByIDDepot(int id)
        {
            return Ok(blforfait.SelectForfaitByIDDepot(id));
        }

        [Route("UpdateForfait/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateForfait(Forfait forfait)
        {
            blforfait.Update(forfait);
            return Ok();
        }

        [Route("PostForfait/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostForfait(Forfait forfait)
        {
            blforfait.Insert(forfait);
            return Ok();
        }

    }
}
