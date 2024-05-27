using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

public class EmailSender : IEmailSender
{
	private readonly EmailSettings _emailSettings;

	public EmailSender(IOptions<EmailSettings> emailSettings)
	{
		_emailSettings = emailSettings.Value;
	}

	public async Task SendEmailAsync(string email, string subject, string htmlMessage)
	{
		var smtpClient = new SmtpClient
		{
			Host = _emailSettings.SmtpServer,
			Port = _emailSettings.Port,
			EnableSsl = true,
			Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password)
		};

		var mailMessage = new MailMessage
		{
			From = new MailAddress(_emailSettings.UserName),
			Subject = subject,
			Body = htmlMessage,
			IsBodyHtml = true
		};

		mailMessage.To.Add(email);

		await smtpClient.SendMailAsync(mailMessage);
	}
}
