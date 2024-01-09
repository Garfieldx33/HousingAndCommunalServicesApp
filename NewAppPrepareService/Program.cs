using CommonLib.Config;
using NewAppPrepareService;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Data Access Web Service init");

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        IConfiguration configuration = context.Configuration;
        services.Configure<RabbitMqConfig>(
          context.Configuration.GetSection("RabbitMQConf"));
        services.Configure<Dictionary<string,string>>(
          context.Configuration.GetSection("SaveMethodURLs"));
        services.AddHostedService<NewAppListener>();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    })
    .UseNLog()
    .UseWindowsService()
    .Build();


await host.RunAsync();
