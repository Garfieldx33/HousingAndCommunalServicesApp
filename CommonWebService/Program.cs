using CommonLib.Config;
using CommonWebService;
using CommonWebService.Services;
using NLog;
using NLog.Web;
using System.Net;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
Logger _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
ConfigurationManager configuration = builder.Configuration;

builder.Services.Configure<gRpcConfig>(
           configuration.GetSection("gRpcConfig"));
_logger.Debug(configuration.GetSection("gRpcConfig").GetSection("HttpsEndpoint").Value);
builder.Services.AddAutoMapper(typeof(WebApiMappingProfile));
builder.Services.AddScoped<AppServiceGrpc>();
builder.Services.AddScoped<UserServiceGrpc>();
builder.Services.AddScoped<DictionaryServiceGrpc>();
builder.Services.AddScoped<EmployeeServiceGrpc>();

builder.Services.AddControllers().AddJsonOptions(options => 
{ 
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
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

//app.UseHttpsRedirection();
app.MapControllers();
app.Run();