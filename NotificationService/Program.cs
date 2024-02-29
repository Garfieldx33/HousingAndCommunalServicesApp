using NotificationService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<NotificationWorker>();
    })
    .Build();

await host.RunAsync();
