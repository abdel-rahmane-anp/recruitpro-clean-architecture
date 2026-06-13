using RecruitProApp.Application.Common.Interfaces;

namespace RecruitProApp.Infrastructure.Services
{
    public class EmailCustomService : IEmailCustomService
    {
        public async Task SendAsync(string to, string subject, string body)
        {
            // Mock 
            await Task.Run(() =>
            {
                Console.WriteLine($"Mail envoyé à {to} : {subject}\n{body}");
            });

            // Pour implémentation SMTP réelle, utiliser SmtpClient, SendGrid, etc.
        }
    }
}
