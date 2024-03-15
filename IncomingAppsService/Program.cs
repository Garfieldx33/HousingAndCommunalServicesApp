using CommonLib.Config;
using CommonLib.DAL;
using IncomingAppsService;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Data Access Web Service init");

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddLogging(log => log.AddNLog());
    
    builder.Services.Configure<RabbitMqConfig>(
           configuration.GetSection("RabbitMQConf"));

    builder.Services.AddScoped<RabbitMqPublisherService>();
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    builder.Services.AddControllers(options =>
    {
        options.AllowEmptyInputInBodyModelBinding = true;
    });
    builder.Services.AddControllers().AddNewtonsoftJson();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.WebHost.ConfigureKestrel((context, options) =>
    {
        options.Listen(IPAddress.Any, 7770);
        options.Listen(IPAddress.Any, 7771);
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex);
    throw;
}
finally
{
    LogManager.Shutdown();
}
