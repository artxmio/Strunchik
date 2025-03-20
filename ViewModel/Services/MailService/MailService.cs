using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Windows;

namespace Strunchik.ViewModel.Services.MailService;

public class MailService
{
    private readonly string SmtpServer;
    private readonly int SmtpPort;
    private readonly string SenderEmail;
    private readonly string SenderPassword;

    public MailService()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile(@"Resources/Other/app.config.json")
            .Build();

        var smtpSettings = configuration.GetSection("SmtpSettings");

        if (smtpSettings is not null)
        {
            SmtpServer = smtpSettings["Server"];
            SmtpPort = int.Parse(smtpSettings["Port"]);
            SenderEmail = smtpSettings["Email"];
            SenderPassword = smtpSettings["Password"];
        }
        else
        {
            throw new InvalidProgramException();
        }
    }

    public async Task SendEmailAsync(string recipientEmail, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Магазин", SenderEmail));
        message.To.Add(new MailboxAddress("", recipientEmail));
        message.Subject = "Спасибо за ваш заказ!";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = body
        };

        bodyBuilder.Attachments.Add(@"temp\ShopReceipt.pdf");

        message.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(SmtpServer, SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(SenderEmail, SenderPassword);
            await client.SendAsync(message);
        }
        catch (AuthenticationException authEx)
        {
            MessageBox.Show($"Ошибка авторизации: {authEx.Message}");
            throw new AuthenticationException();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Произошла ошибка: {ex.Message}");
            throw new Exception();
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}
