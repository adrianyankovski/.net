namespace Proekt.API.Models
{
	public class ClientDTO
	{

		public int Id { get; set; }
		public string Name { get; set; } =string.Empty;

		public string Description { get; set; }

		public int Age	{ get; set; }

		public bool Allowed { get; set; }

		public int ActivityCount
		{
			get
			{
				return Activity.Count;
			}
		}
		public ICollection<ActivityDTO> Activity { get; set; }
	     = new List<ActivityDTO>();
	}
}
