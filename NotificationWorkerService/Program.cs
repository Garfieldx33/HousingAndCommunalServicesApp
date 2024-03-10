using CommonLib.Config;
using NotificationWorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        IConfiguration configuration = context.Configuration;
        services.Configure<RabbitMqConfig>(
          context.Configuration.GetSection("RabbitMQConf"));
        services.Configure<SmtpConfig>(
                   configuration.GetSection("SmtpConfig"));
        services.AddHostedService<EmailNotificationSerivce>();
    })
    .Build();

await host.RunAsync();
