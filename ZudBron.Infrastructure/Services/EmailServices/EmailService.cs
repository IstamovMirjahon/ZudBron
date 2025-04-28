using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using ZudBron.Application.IService.IEmailServices;
using ZudBron.Domain.StaticModels.SmtpModel;

namespace ZudBron.Infrastructure.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
             _smtpSettings = smtpSettings.Value;
        }
        public async Task SendMessageEmail(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress(toEmail, toEmail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = body };

            using var smtpClient = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtpClient.Timeout = 30000; // 30 sekund    
                await smtpClient.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await smtpClient.SendAsync(message);
            }
            catch (SmtpCommandException ex)
            {
                Console.WriteLine($"SMTP buyruq xatoligi: {ex.Message}");
            }
            catch (SmtpProtocolException ex)
            {
                Console.WriteLine($"SMTP protokol xatoligi: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email jo‘natishda umumiy xatolik: {ex.Message}");
            }
            finally
            {
                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
