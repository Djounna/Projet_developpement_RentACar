using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_RAC.Controllers
{
    [Route("api/[controller]")]
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
    }
}
