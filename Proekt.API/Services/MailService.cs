namespace Proekt.API.Services
{
	public class MailService : IMailService
	{
		private readonly string _mailTo = string.Empty;
		private readonly string _mailFrom = string.Empty;

		public MailService(IConfiguration configuration)
		{
			_mailTo = configuration["mailSettings:mailToAdress"];
			_mailFrom = configuration["mailSettings:mailFromAdress"];
		}
			public void Send(string subject, string message)
		{

			Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " +
				$"with {nameof(MailService)}.");
			Console.WriteLine($"Subject: {subject}");
			Console.WriteLine($"Message: {message}");
		}

	}
}
