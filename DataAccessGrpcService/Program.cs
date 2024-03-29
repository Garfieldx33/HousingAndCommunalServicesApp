using CommonLib.Config;
using CommonLib.DAL;
using DataAccessGrpcService;
using DataAccessGrpcService.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Data Access gRPC Service init");

builder.Logging.ClearProviders();
builder.Host.UseNLog();
try
{
    builder.Services.Configure<RabbitMqConfig>(
       configuration.GetSection("RabbitMQConf"));

    builder.Services.AddLogging(log => log.AddNLog());
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("SrcConnectionString"));
    });
    builder.Services.AddScoped<PostgresRepository>();
    builder.Services.AddScoped<NotificationQueueService>();
    builder.Services.AddAutoMapper(typeof(GrpcMappingProfile));
    builder.Services.AddGrpc();

    var app = builder.Build();

    app.MapGrpcService<DataAccessGrpcBase>();
    app.MapGet("/", () => "Use GRPC");

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
