namespace ZudBron.Application.IService.IEmailServices
{
    public interface IEmailService
    {
        Task SendMessageEmail(string toEmail, string subject, string body);
    }
}
