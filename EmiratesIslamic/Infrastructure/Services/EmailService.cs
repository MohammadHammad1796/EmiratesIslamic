using EmiratesIslamic.Core.Helpers;
using EmiratesIslamic.Core.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace EmiratesIslamic.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

            mimeMessage.To.Add(new MailboxAddress(email, email));

            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart("html")
            {
                Text = htmlMessage
            };

            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = (_, _, _, _) => true;

            await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, true);

            await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

            await client.SendAsync(mimeMessage);

            await client.DisconnectAsync(true);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}