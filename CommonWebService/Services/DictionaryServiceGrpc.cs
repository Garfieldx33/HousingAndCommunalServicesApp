using CommonLib.Config;
using CommonLib.Entities;
using CommonLib.Enums;
using CommonLib.Helpers;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommonWebService.Services;

public class DictionaryServiceGrpc
{
    private gRpcConfig _gRpcConfig;
    public DictionaryServiceGrpc(IOptions<gRpcConfig> gRpcConfigSection)
    {
        _gRpcConfig = gRpcConfigSection.Value;
    }

    #region Departments
    public Task<List<Department>> GetAllDepartmentsAsync()
    {
        List<Department> resultList = new();
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.GetDepartments(new DepartmentsRequest());
        foreach(var dept in reply.Departments)
        {
            resultList.Add(new Department { Id = dept.DepartmentId, Name = dept.DepartmentName });
        }
        return Task.FromResult(resultList);
    }

    public Task<Department> AddDepartmentAsync(string departmentName)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.AddDepartment(new DepartmentRequest { DepartmentName = departmentName });
        return Task.FromResult(new Department { Id = reply.DepartmentId, Name = reply.DepartmentName });
    }

    public Task<Department> GetDepartmentByIdAsync(int departmentId)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.GetDepartmentById(new DepartmentRequest { DepartmentId = departmentId });
        return Task.FromResult(new Department { Id = reply.DepartmentId, Name = reply.DepartmentName });
    }

    public Task<string> UpdateDepartmentAsync(Department department)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.UpdateDepartment(new DepartmentRequest { DepartmentId = department.Id, DepartmentName = department.Name });
        return Task.FromResult(reply.DepartmentName);
    }

    public Task<Department> DeleteDepartmentAsync(Department department)
    {
        using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
        var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
        var reply = client.DeleteDepartment(new DepartmentRequest { DepartmentId = department.Id, DepartmentName = department.Name });
        return Task.FromResult(new Department { Id = reply.DepartmentId, Name = reply.DepartmentName});
    }
    #endregion

    #region Enums

    public Task<Dictionary<int,string>> GetAppStatusesAsync()
    {
        return Task.FromResult(EnumConverter.EnumToDictionary<AppStatusEnum>());
    }

    public Task<Dictionary<int, string>> GetAppTypesAsync()
    {
        return Task.FromResult(EnumConverter.EnumToDictionary<AppTypeEnum>());
    }

    public Task<Dictionary<int, string>> GetUserTypesAsync()
    {
        return Task.FromResult(EnumConverter.EnumToDictionary<UserTypeEnum>());
    }

    #endregion

}
