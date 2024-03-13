using AutoMapper;
using CommonLib.Config;
using CommonLib.Entities;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CommonWebService.Services
{
    public class EmployeeServiceGrpc
    {
        private readonly gRpcConfig _gRpcConfig;
        private readonly IMapper _mapper;

        public EmployeeServiceGrpc(IOptions<gRpcConfig> gRpcConfigSection, IMapper mapper)
        {
            _gRpcConfig = gRpcConfigSection.Value;
            _mapper = mapper;
        }

        public Task<string> AddEmployee(EmployeeInfo newEmployee)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.AddEmployee(_mapper.Map<EmployeeInfoGrpc>(newEmployee));
            return Task.FromResult(reply.OperationResult);
        }

        public Task<List<User>> GetEmployersByDepartmentId(int deptId)
        {
            List<User> resultList = new();
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.GetEmployersInfoByDepartmentId(new EmployeeInfoRequest { SearchingId = deptId});
            foreach( var user in reply.Users)
            {
                resultList.Add(_mapper.Map<User>(user));
            }
            return Task.FromResult(resultList);
        }

        public Task<string> GetDepartmentByUserId(int deptId)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.GetDepartmentByUserId(new EmployeeInfoRequest { SearchingId = deptId });
            return Task.FromResult(reply.DepartmentName);
        }

        public Task<string> UpdateEmployee(EmployeeInfo employeeInfo)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.UpdateEmployee(_mapper.Map<EmployeeInfoGrpc>(employeeInfo));
            return Task.FromResult(reply.OperationResult);
        }

        public Task<string> DeleteEmployee(int employeeUserId)
        {
            using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
            var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
            var reply = client.DeleteEmployee(new EmployeeInfoRequest { SearchingId = employeeUserId });
            return Task.FromResult(reply.OperationResult);
        }
    }
}
