using Microsoft.AspNetCore.Mvc;
using Proekt.API.Models;
using System.Reflection.Metadata.Ecma335;

namespace Proekt.API.Controllers

{
	[ApiController]
	[Route("api/clients")]
	public class DiskotekaControllercs : ControllerBase
	{
		private readonly ClientsDataStore _clientsDataStore;
		public DiskotekaControllercs(ClientsDataStore clientsDataStore)
		{
			_clientsDataStore = clientsDataStore ?? throw new ArgumentNullException(nameof(clientsDataStore));
		}
		
			[HttpGet]
		public ActionResult<IEnumerable<ClientDTO>> GetClients()
		{
			return Ok(_clientsDataStore.Clients);
		}

		[HttpGet("{id}")]
		public ActionResult<ClientDTO> GetClient(int id)
		{
			var clientToReturn = _clientsDataStore.Clients
				.FirstOrDefault(c => c.Id == id);

			if (clientToReturn == null)
			{
				return NotFound();
			}
			return Ok(clientToReturn);
	
		}

		[HttpPatch("{clientid}")]
		public ActionResult ChangeBanStatus(int clientId)
		{
			var clientToReturn = _clientsDataStore.Clients
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
