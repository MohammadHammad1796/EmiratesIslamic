using System.Threading.Tasks;

namespace EmiratesIslamic.Core.Services;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
}