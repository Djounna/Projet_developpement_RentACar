using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;

namespace API_RAC.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoueurController : ControllerBase
    {
        
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

       
        /*[Route("GetPaysById/")]
        [HttpGet("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pays>> GetPaysById(int id)
        {

            BLPays lstPays = new BLPays();
            List<Pays> pays = new List<Pays>();
            pays = lstPays.GetAllPays();

            return Ok(pays);

        }

        [Route("UptadePays/")]
        [HttpPut("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pays>> UptadePays(int id, Pays pays)
        {

            BLPays lstPays = new BLPays();
            

            return Ok(pays);

        }*/


        [Route("PostPays/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
   
        public ActionResult PostPays(Pays pays)
        {
           
            BLPays blPays = new BLPays();
            blPays.CreatePays(pays);
            return Ok();
           

        }

       /* [Route("DeletePays/")]
        [HttpDelete("id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Pays>>> DeletePays(int id)
        {

            BLPays lstPays = new BLPays();
            List<Pays> pays = new List<Pays>();
            pays = lstPays.GetAllPays();

            return Ok(pays);

        }*/


    }
}
