using Proekt.API.Models;

namespace Proekt.API
{
	public class ClientsDataStore
	{
		public List<ClientDTO> Clients { get; set; }

		public static ClientsDataStore Current { get; } = new ClientsDataStore();

		public ClientsDataStore()
		{
			Clients = new List<ClientDTO>()
			{
				new ClientDTO()
				{
					Id =1,
					Name = "Silvester Brabursa",
					Description = "Краден мерджан",
					Age = 20,
					Allowed = false,
						Activity = new List<ActivityDTO>()
				{
					new ActivityDTO()
					{
						Id = 1,
						Date = "12.01.2022",
						Bill = 200,
						ProblemsCaused = "Sedq na bara cqla vecher , izgoniha go s preduprejdenie",
						DiscountForNextTime = false

					},

					new ActivityDTO()
					{
						Id = 2,
						Date = "1.02.2023",
						Bill = 1700,
						ProblemsCaused = "Subra cqlata mahla, horata si trugnaha, beshe fanat da krade portfeili. Bannat zavinagi",
						DiscountForNextTime = false


						}
					}
				},

				new ClientDTO()
				{
					Id = 2,
					Name = "Ivo Avramov",
					Description = "Tegav, trepe sichko nared dori i ohranite",
					Age = 19,
					Allowed = true,
					Activity = new List<ActivityDTO>()
				{
					new ActivityDTO()
					{
						Id = 1,
						Date = "10.05.2023",
						Bill = 500,
						ProblemsCaused = "Piqn na kirka, zbi se otvunka , postradha dvama taksidjii",
						DiscountForNextTime = false

					},

					new ActivityDTO()
					{
						Id = 2,
						Date = "10.09.2023",
						Bill = 1000,
						ProblemsCaused = "Svqlqshe serviotiorkata, prebi ohranata poneje mu se izdurvi.",
					
						DiscountForNextTime = false


						}
					}
				},

				new ClientDTO()
				{
				Id = 3,
				Name = "Don Bankya",
				Description = "Pravi se na chujdenec ama dori bulgarski ne znae, ima pari",
				Age = 35,
				Allowed = true,
				Activity = new List<ActivityDTO>()
				{
					new ActivityDTO()
					{
						Id = 1,
						Date = "14.07.2023",
						Bill = 1400,
						ProblemsCaused = "Schupi separeto, izgoni jenite ama si plati otgore",
						DiscountForNextTime = true

					},

					new ActivityDTO()
					{
						Id = 2,
						Date = "28.08.2023",
						Bill = 11400,
						ProblemsCaused = "Izkupi celiq serviz, svurshi shampanskoto",
						DiscountForNextTime = true

					}
				}
				}
			};
		}
	}
}
