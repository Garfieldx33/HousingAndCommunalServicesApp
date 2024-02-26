using CommonLib.Config;
using CommonLib.Entities;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace StaffService.Services
{
    public class StaffSerciceGrpc
    {
        public Application GetApplicationById(int id)
        {
            // создаем канал для обмена сообщениями с сервером
            // параметр - адрес сервера gRPC
            using var channel = GrpcChannel.ForAddress("https://localhost:7062");
            // создаем клиент
            var client = new DataAccessGrpcClient.DataAccessGrpcClientClient(channel);

            //// обмениваемся сообщениями с сервером
            //var reply = await client.SayHelloAsync(new HelloRequest { Name = name });
            //Console.WriteLine($"Ответ сервера: {reply.Message}");

            var reply = client.GetAppsByUserId(new GetAppsByUserIdRequest());
            var app = new Application();
            app.Id = reply.Applications.First().Id;

            return app;
        }

    }
}
