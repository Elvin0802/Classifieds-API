using ClassifiedsApp.Application.Interfaces.Services.Mail;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ClassifiedsApp.Infrastructure.Services.Mail;

public class MailService : IMailService
{
	readonly IConfiguration _configuration;

	public MailService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
	{
		await SendMailAsync([to], subject, body, isBodyHtml);
	}

	public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
	{
		MailMessage mail = new();
		mail.IsBodyHtml = isBodyHtml;

		foreach (var to in tos)
			mail.To.Add(to);

		mail.Subject = subject;
		mail.Body = body;

		mail.From = new(_configuration["Mail:Username"]!, "Classifieds App", Encoding.UTF8);

		SmtpClient smtp = new();

		smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);

		smtp.Port = 587;
		smtp.EnableSsl = true;

		smtp.Host = _configuration["Mail:Host"]!;

		await smtp.SendMailAsync(mail);
	}

	public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
	{
		StringBuilder mail = new();

		mail.AppendLine("Salam<br>Eger shifrenizi yenilemek isteyirsinizse, ashagidaki linke daxil olun.<br><strong><a target=\"_blank\" href=\"");
		mail.AppendLine(_configuration["ClientUrl"]);
		mail.AppendLine("/update-password/");
		mail.AppendLine(userId);
		mail.AppendLine("/");
		mail.AppendLine(resetToken);
		mail.AppendLine("\">Shifreni yenilemek ucun click edin...</a></strong><br><br><span style=\"font-size:16px;\">DIQQET : Eger her hansi shifre yenileme isteyiniz olmayibsa, bu maili gormezden gelin..</span><br>Teshekkurler...<br><br><br>Classifieds App");

		await SendMailAsync(to, "Reset Password Request", mail.ToString());
	}
}