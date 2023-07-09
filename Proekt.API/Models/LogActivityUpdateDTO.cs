using System.ComponentModel.DataAnnotations;

namespace Proekt.API.Models
{
	public class LogActivityupdateDTO
	{
		[Required(ErrorMessage = " A date is needed")]
		[MaxLength(10)]
		public string Date { get; set; }

		[Required(ErrorMessage = " A bill is needed")]
		public int Bill { get; set; }
		[MaxLength(200)]
		public string ProblemsCaused { get; set; } = string.Empty;

		[Required(ErrorMessage = " Discount status is needed")]
		public bool DiscountForNextTime
		{
			get; set;
		}
	}
}