using CommonLib.Abstracts;
using CommonLib.Config;
using CommonLib.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using System.Net;
using System.Net.Mail;

namespace NotificationWorkerService;

public class EmailNotificationSerivce : NotificationServiceBase, IHostedService
{
    Logger _logger = LogManager.Setup().GetCurrentClassLogger();
    private SmtpConfig _smtpConfig;

    public EmailNotificationSerivce(IOptions<RabbitMqConfig> rabbitMqConfig, IOptions<SmtpConfig> smtpConfig) : base(rabbitMqConfig)
    {
        _smtpConfig = smtpConfig.Value;
    }

    public override bool SendMessage(MessageDTO message)
    {
        if (message.MessagingMethod == "email")
        {
            try
            {
                MailAddress Sender = new(_smtpConfig.SourceEmailAddress);
                MailAddress Reciever = new(message.Destination);
                MailMessage Message = new(Sender, Reciever)
                {
                    Subject = message.Subject,
                    Body = message.Body
                };

                SmtpClient smtp = new SmtpClient(_smtpConfig.SmtpServerAddress, _smtpConfig.SmtpPort)
                {
                    Credentials = new NetworkCredential(_smtpConfig.SourceEmailAddress, _smtpConfig.SourceEmailPwd),
                    EnableSsl = true
                };
                smtp.Send(Message);
            }
            catch 
            (Exception ex) 
            {
                _logger.Warn(ex);
                return false;
            }
        }
        return true;
    }

    Task IHostedService.StartAsync(CancellationToken cancellationToken)
    {
        return StartAsync(cancellationToken);
    }

    Task IHostedService.StopAsync(CancellationToken cancellationToken)
    {
        return StopAsync(cancellationToken);
    }
}
