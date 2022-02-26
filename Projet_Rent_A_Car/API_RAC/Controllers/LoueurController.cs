﻿using BusinessLayer;
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

        [Route("GetNotorieteByID/{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Notoriete>> GetNotorieteByID(int id)
        {

            BLNotoriete lstNotoriete = new BLNotoriete();
            Notoriete notoriete = new Notoriete();
            notoriete = lstNotoriete.GetPaysByID(id);
            return Ok(notoriete);

        }


        [Route("UptadeNotoriete/")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UptadePays(Notoriete notoriete)
        {

            BLNotoriete blNotoriete= new BLNotoriete();
            blNotoriete.UptadeNotoriete(notoriete);
            return Ok();

        }


        [Route("PostNotoriete/")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public ActionResult PostPays(Notoriete notoriete)
        {

            BLNotoriete notoriete = new BLNotoriete();
            blNotoriete.CreatePays(notoriete);
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



    }
}
