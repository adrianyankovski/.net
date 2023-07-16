using Microsoft.AspNetCore.StaticFiles;
using Proekt.API.Services;
using Serilog;
using Proekt.API.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Proekt.API
{
	public class Program
	{
		public static void Main(string[] args)
		{

			Log.Logger = new LoggerConfiguration().
				MinimumLevel.Debug().
				WriteTo.Console().
				WriteTo.File("logs/clientinfo.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();

			var builder = WebApplication.CreateBuilder(args);
			//builder.Logging.ClearProviders();
			//builder.Logging.AddConsole();
			builder.Host.UseSerilog();
			// Add services to the container.

			builder.Services.AddControllers(options => 
			{
				options.ReturnHttpNotAcceptable = true;
			})
				.AddXmlDataContractSerializerFormatters();
				//.AddNewtonsoftJson();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

#if DEBUG
			builder.Services.AddTransient<IMailService, MailService>();
#else
			builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

			builder.Services.AddSingleton<ClientsDataStore>();

			builder.Services.AddDbContext<ClientInfoContext>(
				dbContextOptions => dbContextOptions.UseSqlite("Data Source=ClientInfo.db"));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.MapControllers(); 


			app.Run();
		}
	}
}