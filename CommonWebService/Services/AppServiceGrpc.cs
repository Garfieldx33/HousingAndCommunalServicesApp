using AutoMapper;
using CommonLib.Config;
using CommonLib.DTO;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CommonWebService.Services
{
    public class AppServiceGrpc
    {
        gRpcConfig _gRpcConfig;
        IMapper _mapper;
        public AppServiceGrpc(IOptions<gRpcConfig> gRpcConfigSection, IMapper mapper)
        {
            _gRpcConfig = gRpcConfigSection.Value;
            _mapper = mapper;
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

        public Task<string> UpdateAppAsync(UpdateAppDto request)
        {
            var updateAppRequest = _mapper.Map<UpdateAppRequest>(request);
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.UpdateApp(updateAppRequest);
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
