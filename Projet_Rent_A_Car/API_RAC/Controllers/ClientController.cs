using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [Route("GetClientByMail/{Mail}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Client>> GetClientByMail(string mail)//OK Antoine
        {
            return Ok(blclient.SelectClientByMail(mail));
        }

        [Route("PostClient/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostClient(Client client)//OK Antoine
        {
            blclient.CreateClient(client);
            return Ok();
        }
    }
}
