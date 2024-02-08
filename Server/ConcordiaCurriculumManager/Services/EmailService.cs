using ConcordiaCurriculumManager.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ConcordiaCurriculumManager.Services;

public interface IEmailService
{
    public Task<bool> SendEmail(string recipientEmail, string subject, string body, bool isBodyHtml = true);
}

public class EmailService : IEmailService
{
    private readonly ILogger<IEmailService> _logger;
    private readonly SenderEmailSettings _senderEmailSettings;

    public EmailService(ILogger<IEmailService> logger, IOptions<SenderEmailSettings> options)
    {
        _logger = logger;
        _senderEmailSettings = options.Value ?? throw new ArgumentNullException("Sender Email Settings cannot be null");
    }

    public async Task<bool> SendEmail(string recipientEmail, string subject, string body, bool isBodyHtml = false)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Sender Name", _senderEmailSettings.SenderEmail));
        message.To.Add(new MailboxAddress("Recipient Name", recipientEmail));
        message.Subject = subject;

        if (isBodyHtml)
        {
            message.Body = new TextPart("html")
            {
                Text = body
            };
        }
        else
        {
            message.Body = new TextPart("plain")
            {
                Text = body
            };
        }

        try
        {
            using var client = new SmtpClient();
            await client.ConnectAsync(_senderEmailSettings.SenderSMTPHost, _senderEmailSettings.SenderSMTPPort, false);
            await client.AuthenticateAsync(_senderEmailSettings.SenderEmail, _senderEmailSettings.SenderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            _logger.LogInformation($"Sent an email to {recipientEmail} succesfully");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Failed to send an email to {recipientEmail}: {e.Message}");
            return false;
        }
    }

}
