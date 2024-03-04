using NotificationService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<IncomingNotificationWorker>();
    })
    .Build();

await host.RunAsync();
