namespace Proekt.API.Models
{
	public class ActivityDTO
	{

	public int Id { get; set; }
		public string Date { get; set; }
		public int Bill { get; set; }

		public string ProblemsCaused { get; set; } = string.Empty;

		public bool DiscountForNextTime { get; set; }	
	}
}
