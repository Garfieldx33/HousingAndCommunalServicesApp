using CommonLib.Config;
using CommonLib.Entities;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CommonWebService.Services
{
    public class UserServiceGrpc
    {
        gRpcConfig _gRpcConfig;
        public UserServiceGrpc(IOptions<gRpcConfig> gRpcConfigSection)
        {
            _gRpcConfig = gRpcConfigSection.Value;
        }

        public Task<string> GetAllUsersAsync()
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.GetUsers(new UsersRequest());
            return Task.FromResult(JsonConvert.SerializeObject(reply.Users));
        }

        public Task<string> GetUserAsync(UserDtoGrpc user)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.GetUser(new UserDtoRequest { UserDto = user });
            return Task.FromResult(JsonConvert.SerializeObject(reply.User));
        }

        public Task<string> AddUserAsync(UserGrpc newUser)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.AddUser(new UserRequest { User = newUser });
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }

        public Task<string> UpdateUserAsync(UserGrpc user)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.UpdateUser(new UserRequest { User = user });
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }

        public Task<string> DeleteUserAsync(UserDtoRequest user)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.DeleteUser(user);
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }
    }
}
