using CommonLib.DAL;
using DataAccessWebService;
using Microsoft.EntityFrameworkCore;
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
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseNpgsql(configuration.GetConnectionString("SrcConnectionString"));
    });
    builder.Services.AddScoped<PostgresRepository>();
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddControllers(options =>
    {
        options.AllowEmptyInputInBodyModelBinding = true;
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    builder.WebHost.ConfigureKestrel((context, options) =>
    {
        options.Listen(IPAddress.Any, 7000);
        options.Listen(IPAddress.Any, 7001);
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
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





