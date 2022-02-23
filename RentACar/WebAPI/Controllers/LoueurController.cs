using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.BusinessLayer;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/Loueur")]
    [ApiController]
    public class LoueurController : ControllerBase
    {

        [Produces("application/json")]
        [Route("GetPays/")]
        [HttpGet]

        public async Task<ActionResult<List<Pays>>> GetPays()
        {

            BLPays lstPays = new BLPays();
            List<Pays> pays = new List<Pays>();
            pays = lstPays.GetAllPays();

            return Ok(pays);

        }

        /*
        public async Task<ActionResult<List<Pays>>> GetPaysByID()
        {
            return Ok();
        }
        public async Task<ActionResult<List<Pays>>> PosttPays()
        {
            return Ok();
        }
        */
    }
}
