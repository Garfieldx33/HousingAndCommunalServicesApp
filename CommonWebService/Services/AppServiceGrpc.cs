using AutoMapper;
using CommonLib.Config;
using CommonLib.DTO;
using CommonLib.Entities;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CommonWebService.Services;

public class AppServiceGrpc
{
    gRpcConfig _gRpcConfig;
    IMapper _mapper;
    public AppServiceGrpc(IOptions<gRpcConfig> gRpcConfigSection, IMapper mapper)
    {
        _gRpcConfig = gRpcConfigSection.Value;
        _mapper = mapper;
    }

    public Task<List<Application>> GetAppsByUserIdAsync(int userId)
    {
        List<Application> resultList = new();
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.GetAppsByUserId(new GetAppsByUserIdRequest { ApplicantId = userId});
        if (reply is not null)
        {
            foreach (var app in reply.Applications)
            {
                resultList.Add(_mapper.Map<Application>(app));
            } 
        }
        return Task.FromResult(resultList);
    }

    public Task<List<Application>> GetOpenedAppsByDepartmentIdAsync(int departmentId)
    {
        List<Application> resultList = new();
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.GetOpenedAppsByDepartmentId(new GetOpenAppsByDepartmentIdRequest { DepartmentId = departmentId });
        if (reply is not null)
        {
            foreach (var app in reply.Applications)
            {
                resultList.Add(_mapper.Map<Application>(app));
            }
        }
        return Task.FromResult(resultList);
    }

    public Task<List<Application>> GetAppsByExecutorIdAsync(int executorId)
    {
        List<Application> resultList = new(); 
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.GetAppsByExecutorId(new GetAppsByExecutorIdRequest { ExecutorId = executorId });
        if (reply is not null)
        {
            foreach (var app in reply.Applications)
            {
                resultList.Add(_mapper.Map<Application>(app));
            }
        }
        return Task.FromResult(resultList);
    }

    public async Task<Application> GetApplicationByIdAsync(int appId) 
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = await client.GetAppByAppIdAsync(new GetAppsByAppIdRequest { Id = appId });
        var replyResult = _mapper.Map<Application>(reply);
        return replyResult;

    }

    /*public Task<string> AddAppAsync(AddNewAppRequest request) //Depricated
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.AddNewApp(request);
        return Task.FromResult(JsonConvert.SerializeObject(reply));
    }*/

    public Task<Application> UpdateAppAsync(UpdateAppDTO request)
    {
        var updateAppRequest = _mapper.Map<UpdateAppRequest>(request);
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.UpdateApp(updateAppRequest);
        return Task.FromResult(_mapper.Map<Application>(reply.UpdatedApplication));
    }

    public Task<(int, string)> DeleteAppAsync(DeleteAppRequest request)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.DeleteApp(request);
        return Task.FromResult((reply.DeletedAppId, reply.Message));
    }

}
