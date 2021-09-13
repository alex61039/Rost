using System.Threading.Tasks;

namespace Rost.Services.Infrastructure
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}