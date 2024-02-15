using CommonLib.Config;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CommonWebService.Services
{
    public class AppServiceGrpc
    {
        gRpcConfig _gRpcConfig;
        public AppServiceGrpc(IOptions<gRpcConfig> gRpcConfigSection)
        {
            _gRpcConfig = gRpcConfigSection.Value;
        }

        public Task<string> GetAppAsync(int userId)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.GetAppsByUserId(new GetAppsByUserIdRequest { ApplicantId = userId});
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }

        public Task<string> AddAppAsync(AddNewAppRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.AddNewApp(request);
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }

        public Task<string> UpdateAppAsync(UpdateAppRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.UpdateApp(request);
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }

        public Task<string> DeleteAppAsync(DeleteAppRequest request)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.DeleteApp(request);
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }

    }
}
