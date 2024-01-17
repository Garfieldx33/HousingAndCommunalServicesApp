using CommonGrpcService.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddGrpc();
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

app.MapGrpcService<DictionaryGrpcService>();
/*app.MapGrpcService<AdministratorService>();
app.MapGrpcService<EmployerService>();*/
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
