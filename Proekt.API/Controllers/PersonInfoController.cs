using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Proekt.API.Models;
using System.Diagnostics;

namespace Proekt.API.Controllers
{
	[Route("api/clients/{clientid}/personalinfo")]
	[ApiController]
	public class PersonalInfoController : ControllerBase
	{
		[HttpGet]
		public ActionResult<IEnumerable<ActivityDTO>> GetClientInfo(int clientid)
		{
			var client = ClientsDataStore.Current.Clients.FirstOrDefault(c => c.Id == clientid);

			if (client == null)
			{
				return NotFound();
			}

			return Ok(client.Activity);
		}

		[HttpGet("{ActivityID}", Name = "GetActivity")]
		public ActionResult<ActivityDTO> GetActivity(int clientid, int ActivityID)
		{
			var client = ClientsDataStore.Current.Clients.FirstOrDefault(c => c.Id == clientid);
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

			var client = ClientsDataStore.Current.Clients.FirstOrDefault(c => c.Id == clientid);
			if (client == null)
			{
				return NotFound();
			}

			var ActivityReports = ClientsDataStore.Current.Clients.SelectMany(
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
			var client = ClientsDataStore.Current.Clients.FirstOrDefault( c => c.Id == clientid);
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
	}
}