using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proekt.API.Entities

{
	public class Clients
	{

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; } = string.Empty;

		[MaxLength(200)]
		public string Description { get; set; }

		public int Age { get; set; }

		public bool Allowed { get; set; }

		public ICollection<ActivityLogs> UserActivity{ get; set; }
				= new List<ActivityLogs>();

		public Clients(string name) 
		{ 
			Name = name; 
		}	
	}
}
