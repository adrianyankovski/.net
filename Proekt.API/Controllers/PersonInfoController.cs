using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Proekt.API.Models;
using Proekt.API.Services;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Proekt.API.Controllers
{
	[Route("api/clients/{clientid}/personalinfo")]
	[ApiController]
	public class PersonalInfoController : ControllerBase
	{
		private readonly ILogger<PersonalInfoController> _logger;
		private readonly IMailService _mailService;
		private readonly ClientsDataStore _clientsDataStore;

		

		public PersonalInfoController(ILogger<PersonalInfoController> logger, 
			IMailService mailservice,
			ClientsDataStore clientsDataStore)
			{ 
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_mailService = mailservice ?? throw new ArgumentNullException(nameof(mailservice));
			_clientsDataStore = clientsDataStore ?? throw new ArgumentNullException(nameof(clientsDataStore));
		}


		[HttpGet]
		public ActionResult<IEnumerable<ActivityDTO>> GetClientInfo(int clientid)
		{
			try
			{
				var client = _clientsDataStore.Clients.FirstOrDefault(c => c.Id == clientid);

				if (client == null)
				{
					_logger.LogInformation($"Client with {clientid} has no activity history");
					return NotFound();
				}

				return Ok(client.Activity);
			}
			catch (Exception ex)
			{
				_logger.LogCritical($"Exception while getting ClientInfo for client with id {clientid}.", ex);
				return StatusCode(500, "A problem while handling your request");
			}
		}

		[HttpGet("{ActivityID}", Name = "GetActivity")]
		public ActionResult<ActivityDTO> GetActivity(int clientid, int ActivityID)
		{
			var client = _clientsDataStore.Clients.FirstOrDefault(c => c.Id == clientid);
			if (client == null)
			{
				return NotFound();
			}

			var activity = client.Activity.FirstOrDefault(c => c.Id == ActivityID);

			if (activity == null)
			{

				return NotFound();

			}

			return Ok(activity);

		}

		[HttpPost]

		public ActionResult<LogActivityDTO> AddActivityLog(
			int clientid, LogActivityDTO activitylog)
		{

			var client = _clientsDataStore.Clients.FirstOrDefault(c => c.Id == clientid);
			if (client == null)
			{
				return NotFound();
			}

			var ActivityReports = _clientsDataStore.Clients.SelectMany(
				c => c.Activity).Max(p => p.Id);

			var NewReport = new ActivityDTO()
			{
				Id = ++ActivityReports,
				Date = activitylog.Date,
				Bill = activitylog.Bill,
				ProblemsCaused = activitylog.ProblemsCaused,
				DiscountForNextTime = activitylog.DiscountForNextTime

			};

			client.Activity.Add(NewReport);
			return CreatedAtRoute("GetActivity", new
			{
				clientid = clientid,
				ActivityID = NewReport.Id

			},
		NewReport);
		}

		[HttpPatch("ActivityID")]

		public ActionResult UpdateActivityLog(int clientid, int activityid,
			JsonPatchDocument<LogActivityupdateDTO> patchDocument)
		{
			var client = _clientsDataStore.Clients.FirstOrDefault( c => c.Id == clientid);
			if(client == null)
			{
				return NotFound();
			}

			var ActivitySelected = client.Activity.FirstOrDefault(c => c.Id == activityid);
			if (ActivitySelected == null)
			{
				return NotFound();
			}

			var UpdateActivityLog = new LogActivityupdateDTO()
			{
				Date = ActivitySelected.Date,
				Bill = ActivitySelected.Bill,
				ProblemsCaused = ActivitySelected.ProblemsCaused,
				DiscountForNextTime = ActivitySelected.DiscountForNextTime

			};

			patchDocument.ApplyTo(UpdateActivityLog);
			ActivitySelected.Date = UpdateActivityLog.Date;
			ActivitySelected.Bill = UpdateActivityLog.Bill;
			ActivitySelected.ProblemsCaused = UpdateActivityLog.ProblemsCaused;	
			ActivitySelected.DiscountForNextTime = UpdateActivityLog.DiscountForNextTime;

			return Ok(ActivitySelected);
		}

		[HttpDelete("{ActivityID}")]

		public ActionResult DeleteActivities(int clientid, int activityid) {

			var client = _clientsDataStore.Clients.FirstOrDefault
				(c => c.Id == clientid);
			if(client == null) { return NotFound();}
			var ActivitySelected = client.Activity.FirstOrDefault(a => a.Id == activityid);
			if (ActivitySelected == null)
			{
				return NotFound();
			}

			client.Activity.Remove(ActivitySelected);
			_mailService.Send(
				"Client Activity Deleted.",
				$"Activity id {ActivitySelected.Id} was deleted.");
			return NoContent();
		}
	}
}