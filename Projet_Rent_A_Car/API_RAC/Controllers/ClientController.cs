﻿using BusinessLayer;
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
        private BLReservation blReservation = new();


        [Route("GetClientById/{Id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Client>> GetClientById(int Id)// Ok Corentin
        {
            return Ok(blclient.SelectClientById(Id));
        }



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


        // Insert avec une méthode ADO de la DAL. A cause de l'erreur générée par EF sur le double accès dans les dépots.
        [Route("PostReservation/")] // en cour Corentin
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult PostReservation(Reservation Reservation)
        {
            blReservation.Insert(Reservation);
            return Ok();
        }


        [Route("DeleteReservation/{id}")] // En cours Corentin
        [HttpDelete]
        public ActionResult DeleteReservation(int id)
        {
            blReservation.DeleteReservation(id);
            return Ok();
        }

    }
}
