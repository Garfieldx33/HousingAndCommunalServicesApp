using Grpc.Core;
using Grpc.Net.Client;
using System.Collections.Generic;

namespace CommonGrpcService.Services
{
    public class DictionaryGrpcService : DictionaryService.DictionaryServiceClient
    {
        public DictionaryGrpcService() { }

        public Task<string> GetDepartmentByIdAsync(int deprtmentId)
        {
            return Task.Factory.StartNew(() => 
            {
                return "Котельная = 1";
            });
            /*using var channel = GrpcChannel.ForAddress("https://localhost:7045");
            var client = new DictionaryService.DictionaryServiceClient(channel);
            var reply = client.GetDepartamentById(new DepartamentRequest { DepartamentId = deprtmentId });
            return reply.DepartamentName;*/

        }
       
    }
}
