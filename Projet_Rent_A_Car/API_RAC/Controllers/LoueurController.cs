using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;

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
        private BLReservation blReservation = new();


        // *********************************************************************** Notoriete ********************************************************************
        # region Notoriete 
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


        [Route("GetAllNotorieteInList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<SelectListItem> GetAllNotorieteInList()
        {
            return blNotoriete.SelectAllNotorieteInList();
        }

        [Route("AlreadyExistNotoriete/")]
        [HttpPut]
        public ActionResult AlreadyExistNotoriete(Notoriete not)
        {
            if (blNotoriete.AlreadyExist(not) == true)
                return BadRequest();
            else return Ok();


        }

        [Route("UpdateNotoriete/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateNotoriete(Notoriete notoriete)
        {
            if (blNotoriete.InsertOrUpdateNotoriete(notoriete) == true)
                return Ok();
            else
                return BadRequest();

        }

        [Route("PostNotoriete/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult PostNotoriete(Notoriete notoriete)
        {
            if (blNotoriete.InsertOrUpdateNotoriete(notoriete) == true)
                return Ok();
            else
                return BadRequest();
        }
        [Route("DeleteNotoriete/{id}")]
        [HttpDelete]
        public ActionResult DeleteNotoriete(int id)
        {
            if (blNotoriete.DeleteNotoriete(id) == true)
                return Ok();
            else
                return BadRequest();
        }
        #endregion
        // *********************************************************************** Voiture **********************************************************************
        #region Voiture
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

        [Route("AlreadyExistVoiture/")]
        [HttpPut]
        public ActionResult AlreadyExistVoiture(Voiture voit)
        {
            if (blVoiture.AlreadyExist(voit) == true)
                return BadRequest();
            else return Ok();
        }

        [Route("UpdateVoiture/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateVoiture(Voiture Voiture)
        {
            if (blVoiture.InsertOrUpdateVoiture(Voiture) == true)
                return Ok();
            else
                return BadRequest();
        }

        [Route("PostVoiture/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult PostVoiture(Voiture Voiture)
        {
            if (blVoiture.InsertOrUpdateVoiture(Voiture) == true)
                return Ok();
            else
                return BadRequest();
        }
        [Route("DeleteVoiture/{id}")]
        [HttpDelete]
        public ActionResult DeleteVoiture(int id)
        {
            if (blVoiture.DeleteVoiture(id) == true)
                return Ok();
            else
                return BadRequest();
        }
        #endregion
        // *********************************************************************** Pays *************************************************************************
        #region Pays
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

        [Route("AlreadyExistPays/")]
        [HttpPut]
        public ActionResult AlreadyExistPays(Pays pays)
        {
            if (blpays.AlreadyExist(pays) == true)
                return BadRequest();
            else return Ok();
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
                if (blpays.InsertOrUpdatePays(pays) == true)
                    return Ok();
                else
                    return BadRequest();

            }
            return BadRequest();

        }

        [Route("PostPays/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostPays(Pays pays)
        {
            if (blpays.InsertOrUpdatePays(pays) == true)
                return Ok();
            else
                return BadRequest();

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

            if (estLie == false)
            {
                if (blpays.DeletePays(id) == true)
                    return Ok();
                else
                    return BadRequest();
            }
            return BadRequest();
        }
        #endregion
        // *********************************************************************** Prix *************************************************************************
        #region Prix
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
            if (blPrix.UpdatePrix(prix) == true)
                return Ok();
            else
                return BadRequest();

        }

        [Route("PostPrix/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostPrix(Prix Prix)
        {
            if (blPrix.InsertOrUpdatePrix(Prix) == true)
                return Ok();
            else
                return BadRequest();
        }
        #endregion
        // *********************************************************************** Ville ************************************************************************
        #region Ville
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

        [Route("AlreadyExistVille/")]
        [HttpPut]
        public ActionResult AlreadyExistVille(Ville ville)
        {
            if (blville.AlreadyExist(ville) == true)
                return BadRequest();
            else return Ok();
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
                if (blville.InsertOrUpdateVille(ville) == true)
                    return Ok();
                else
                    return BadRequest();
            }
            return BadRequest();

        }

        [Route("PostVille/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostVille(Ville ville)
        {
            if (blville.InsertOrUpdateVille(ville) == true)
                return Ok();
            else
                return BadRequest();
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
                if (blville.DeleteVille(id) == true)
                    return Ok();
                else
                    return BadRequest();
            }
            return BadRequest();

        }
        #endregion
        // *********************************************************************** Depot ************************************************************************
        #region Depot
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
            if (bldepot.InsertOrUpdateDepot(depot) == true)
                return Ok();
            else
                return BadRequest();
        }

        [Route("PostDepot/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostDepot(Depot Depot)
        {
            if (bldepot.InsertOrUpdateDepot(Depot) == true)
                return Ok();
            else
                return BadRequest();
        }

        [Route("DeleteDepot/{id}")]
        [HttpDelete]
        public ActionResult DeleteDepot(int id)
        {
            if (bldepot.DeleteDepot(id) == true)
                return Ok();
            else
                return BadRequest();
        }
        #endregion
        // *********************************************************************** Forfait **********************************************************************
        #region Forfait
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
            if (blforfait.Update(forfait) == true)
                return Ok();
            else
                return BadRequest();
        }

        [Route("PostForfait/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostForfait(Forfait forfait)
        {
            if (blforfait.Insert(forfait) == true)
                return Ok();
            else
                return BadRequest();
        }

        [Route("AlreadyExistForfait/")] // EN test
        [HttpPut]
        public ActionResult AlreadyExistForfait(Forfait forfait)
        {
            if (blforfait.AlreadyExist(forfait))
                return BadRequest();
            else return Ok();
        }

        #endregion
        // *********************************************************************** Reservation ******************************************************************
        #region Reservation 

        [Route("GetReservation/")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Reservation>>> GetReservation()
        {
            return Ok(blReservation.SelectAllReservation());
        }


        [Route("GetReservationByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Reservation>> GetReservationByID(int id)
        {
            return Ok(blReservation.SelectReservationByID(id));
        }


        [HttpGet] // Ok 
        [Route("GetAllReservationNotYetStarted/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Reservation>>> GetAllReservationNotYetStarted()
        {
            try
            {
                return Ok(blReservation.SelectAllReservationNotYetStarted());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]// Ok 
        [Route("GetAllReservationCloturees/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Reservation>>> GetAllReservationCloturees()
        {
            try
            {
                return Ok(blReservation.SelectAllReservationCloturees());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet] // Ok
        [Route("GetAllLocationEnCours/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Reservation>>> GetAllLocationEnCours()
        {
            try
            {
                return Ok(blReservation.SelectAllLocationEnCours());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // A faire

        [Route("UpdateReservation/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateReservation(Reservation reservation)
        {
            if (blReservation.Update(reservation) == true)
                return Ok();
            else
                return BadRequest();
        }

        [Route("StartReservation/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult StartReservation(Reservation reservation)
        {
            if (blReservation.StartReservation(reservation) == true)
                return Ok();
            else
                return BadRequest();
        }

        [Route("CloseReservation/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult CloseReservation(Reservation reservation)
        {
            if (blReservation.CloseReservation(reservation) == true)
                return Ok();
            else
                return BadRequest();
        }






        #endregion


    }
}
