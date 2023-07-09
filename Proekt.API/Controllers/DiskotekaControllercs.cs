﻿using Microsoft.AspNetCore.Mvc;
using Proekt.API.Models;
using System.Reflection.Metadata.Ecma335;

namespace Proekt.API.Controllers

{
	[ApiController]
	[Route("api/clients")]
	public class DiskotekaControllercs : ControllerBase
	{
		[HttpGet]
		public ActionResult<IEnumerable<ClientDTO>> GetClients()
		{
			return Ok(ClientsDataStore.Current.Clients);
		}

		[HttpGet("{id}")]
		public ActionResult<ClientDTO> GetClient(int id)
		{
			var clientToReturn = ClientsDataStore.Current.Clients
				.FirstOrDefault(c => c.Id == id);

			if (clientToReturn == null)
			{
				return NotFound();
			}
			return Ok(clientToReturn);
	
		}

		[HttpPut("{clientid}")]
		public ActionResult ChangeBanStatus(int clientId)
		{
			var clientToReturn = ClientsDataStore.Current.Clients
				.FirstOrDefault(c => c.Id == clientId);

			if (clientToReturn == null)
			{
				return NotFound();
			}

			if(clientToReturn.Allowed == false)
			{
				clientToReturn.Allowed = true;
				return Ok(clientToReturn);
			}
			else
			{
				clientToReturn.Allowed = false;
				return Ok(clientToReturn);
			}
		}

	}
}
