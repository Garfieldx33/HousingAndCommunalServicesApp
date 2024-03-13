using AutoMapper;
using CommonLib.Config;
using CommonLib.DTO;
using CommonLib.Entities;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CommonWebService.Services;

public class UserServiceGrpc
{
    private readonly gRpcConfig _gRpcConfig;
    private readonly IMapper _mapper;
    public UserServiceGrpc(IOptions<gRpcConfig> gRpcConfigSection, IMapper mapper)
    {
        _gRpcConfig = gRpcConfigSection.Value;
        _mapper = mapper;
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        List<User> result = new();
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.GetUsers(new UsersRequest());
        
        if (reply is not null)
        {
            if(reply.Users.Any())
            {
                foreach (var user in reply.Users)
                {
                    result.Add(_mapper.Map<User>(user));
                }
            }
        }
        return Task.FromResult(result);
    }

    public Task<User> GetUserAsync(UserDtoGrpc user)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.GetUser(new UserDtoRequest { UserDto = user });
        return Task.FromResult(_mapper.Map<User>(reply.User));
    }

    public Task<(int, string)> AddUserAsync(UserDTO newUserDto)
    {
        var user = _mapper.Map<UserGrpc>(newUserDto);
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.AddUser(new UserRequest { User = user });
        return Task.FromResult((int.Parse(reply.Identificator),reply.Message));
    }

    public Task<UserDTO> UpdateUserAsync(UserDTO updateUserDto)
    {
        var user = _mapper.Map<UserGrpc>(updateUserDto);
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.UpdateUser(new UserRequest { User = user });
        return Task.FromResult(_mapper.Map<UserDTO>(reply.User));
    }

    public Task<(int, string)> DeleteUserAsync(int deleteUserId)
    {
        UserDtoRequest request = new() { UserDto = new UserDtoGrpc { Id = deleteUserId } };
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.DeleteUser(request);
        return Task.FromResult((int.Parse(reply.Identificator), reply.Message));
    }
}
