namespace Proekt.API.Models
{
	public class LogActivityDTO
	{
		public string Date { get; set; }
		public int Bill { get; set; }

		public string ProblemsCaused { get; set; } = string.Empty;

		public bool DiscountForNextTime
		{
			get; set;
		}
	}
}
