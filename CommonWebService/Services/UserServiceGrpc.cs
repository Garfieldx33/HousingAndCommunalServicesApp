using AutoMapper;
using CommonLib.Config;
using CommonLib.DTO;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CommonWebService.Services
{
    public class UserServiceGrpc
    {
        gRpcConfig _gRpcConfig;
        IMapper _mapper;
        public UserServiceGrpc(IOptions<gRpcConfig> gRpcConfigSection, IMapper mapper)
        {
            _gRpcConfig = gRpcConfigSection.Value;
            _mapper = mapper;
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

        public Task<string> AddUserAsync(UserDTO newUserDto)
        {
            var user = _mapper.Map<UserGrpc>(newUserDto);
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.AddUser(new UserRequest { User = user });
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }

        public Task<string> UpdateUserAsync(UserDTO updateUserDto)
        {
            var user = _mapper.Map<UserGrpc>(updateUserDto);
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.UpdateUser(new UserRequest { User = user });
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }

        public Task<string> DeleteUserAsync(int deleteUserId)
        {
            UserDtoGrpc userDtoGrpc = new UserDtoGrpc { Id = deleteUserId };
            UserDtoRequest request = new UserDtoRequest { UserDto = userDtoGrpc };
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.httpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.DeleteUser(request);
            return Task.FromResult(JsonConvert.SerializeObject(reply));
        }
    }
}
