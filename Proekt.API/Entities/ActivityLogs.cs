using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proekt.API.Entities
{
	public class ActivityLogs
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]

		public int Id { get; set; }
		public string Date { get; set; }
		public int Bill { get; set; }

		[Required]
		[MaxLength(200)]

		public string ProblemsCaused { get; set; }

		public bool DiscountForNextTime { get; set; }

		[ForeignKey("CityId")]

		public Clients? Clients { get; set; }

		public int ClientId { get; set; }

		public ActivityLogs(string problemscaused)
		{
			ProblemsCaused= problemscaused;
		}
	}
}
