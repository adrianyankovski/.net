using Proekt.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Proekt.API.DbContexts
{
	public class ClientInfoContext : DbContext
	{

		public DbSet<Clients> Clients { get; set; } = null!;

		public DbSet<ActivityLogs> ActivityLogs { get; set; } = null!;

		public ClientInfoContext(DbContextOptions<ClientInfoContext> options) 
			:base(options)
		{
		
		}
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlite("connrectionstring");
			//base.OnConfiguring(optionsBuilder);
		//}

	}
}
