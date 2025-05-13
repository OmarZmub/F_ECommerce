using F_ECommerce.Core.ViewModels.EmailVMs;
using F_ECommerce.Infrastructure.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using MimeKit;
namespace F_ECommerce.Infrastructure.Services.Implementations;

public class EmailService : IEmailService
{
   private readonly IConfiguration configuration;
   public EmailService(IConfiguration configuration)
   {
      this.configuration = configuration;
   }
   public async Task SendEmail(EmailVM email)
   {
      MimeMessage message = new();

      message.From.Add(new MailboxAddress("My Ecom", configuration["EmailSetting:From"]));
      message.Subject = email.Subject;
      message.To.Add(new MailboxAddress(email.To, email.To));
      message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
      {
         Text = email.Content
      };
      using (var smtp = new MailKit.Net.Smtp.SmtpClient())
      {
         try
         {
            await smtp.ConnectAsync(
                configuration["EmailSetting:Smtp"],
               int.Parse(configuration["EmailSetting:Port"]), true);
            await smtp.AuthenticateAsync(configuration["EmailSetting:Username"],
                configuration["EmailSetting:Password"]);

            await smtp.SendAsync(message);
         }
         catch (Exception ex)
         {

            throw;
         }
         finally
         {
            smtp.Disconnect(true);
            smtp.Dispose();
         }
      }
   }
}
