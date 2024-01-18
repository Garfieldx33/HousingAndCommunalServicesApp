using Grpc.Core;
using Grpc.Net.Client;
using System.Collections.Generic;

namespace CommonGrpcService.Services
{
    public class DictionaryGrpcService : DictionaryService.DictionaryServiceClient
    {
        public DictionaryGrpcService() { }

        public string GetDepartmentById(int deprtmentId)
        {
            /*using var channel = GrpcChannel.ForAddress("https://localhost:7045");
            var client = new DictionaryService.DictionaryServiceClient(channel);
            var reply = client.GetDepartamentById(new DepartamentRequest { DepartamentId = deprtmentId });
            return reply.DepartamentName;*/

            return "Котельная";
        }
       
    }
}
