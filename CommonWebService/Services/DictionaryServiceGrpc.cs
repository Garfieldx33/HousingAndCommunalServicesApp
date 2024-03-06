using CommonLib.Config;
using CommonLib.Entities;
using CommonLib.Enums;
using CommonLib.Helpers;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CommonWebService.Services;

public class DictionaryServiceGrpc
{
    private gRpcConfig _gRpcConfig;
    public DictionaryServiceGrpc(IOptions<gRpcConfig> gRpcConfigSection)
    {
        _gRpcConfig = gRpcConfigSection.Value;
    }

    #region Departments
    public Task<string> GetAllDepartmentsAsync()
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.GetDepartments(new DepartmentsRequest());
        return Task.FromResult(JsonConvert.SerializeObject(reply.Departments));
    }

    public Task<string> AddDepartmentAsync(string departmentName)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.AddDepartment(new DepartmentRequest { DepartmentName = departmentName });
        return Task.FromResult(reply.DepartmentName);
    }

    public Task<string> GetDepartmentByIdAsync(int departmentId)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.GetDepartmentById(new DepartmentRequest { DepartmentId = departmentId });
        return Task.FromResult(reply.DepartmentName);
    }

    public Task<string> UpdateDepartmentAsync(Department department)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.UpdateDepartment(new DepartmentRequest { DepartmentId = department.Id, DepartmentName = department.Name });
        return Task.FromResult(reply.DepartmentName);
    }

    public Task<string> DeleteDepartmentAsync(Department department)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.DeleteDepartment(new DepartmentRequest { DepartmentId = department.Id, DepartmentName = department.Name });
        return Task.FromResult(reply.DepartmentName);
    }
    #endregion

    #region Enums

    public Task<string> GetAppStatusesAsync()
    {
        return Task.FromResult(JsonConvert.SerializeObject(EnumConverter.EnumToDictionary<AppStatusEnum>()));
    }

    public Task<string> GetAppTypesAsync()
    {
        return Task.FromResult(JsonConvert.SerializeObject(EnumConverter.EnumToDictionary<AppTypeEnum>()));
    }

    public Task<string> GetUserTypesAsync()
    {
        return Task.FromResult(JsonConvert.SerializeObject(EnumConverter.EnumToDictionary<UserTypeEnum>()));
    }

    #endregion

}
