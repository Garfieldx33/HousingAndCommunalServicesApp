using CommonGrpcService.Services;
using CommonLib.Entities;
using Google.Protobuf;
using Grpc.Core;

namespace DataAccessGrpcService.Services
{
    public partial class DataAccessGrpcBase
    {
        public override async Task<DepartmentsReply> GetDepartments(DepartmentsRequest request, ServerCallContext context)
        {
            DepartmentsReply reply = new DepartmentsReply();
            List<Department> depts = await _repository.GetAllDepartamentsAsync();
            depts.ForEach(dept => reply.Departments.Add(_mapper.Map<DepartmentGrpc>(dept)));
            return reply;
        }

        public override async Task<DepartmentReply> GetDepartmentById(DepartmentRequest request, ServerCallContext context)
        {
            var dept = await _repository.GetDepartamentByIdAsync(request.DepartmentId);
            DepartmentReply reply = new() { DepartmentId = dept.Id, DepartmentName = dept.Name };
            return reply;
        }

        public override async Task<DepartmentReply> AddDepartment(DepartmentRequest request, ServerCallContext context)
        {
            var dept = await _repository.AddDepartamentAsync(request.DepartmentName);
            DepartmentReply reply = new() { DepartmentId = dept.Id, DepartmentName = dept.Name };
            return reply;
        }

        public override async Task<DepartmentReply> UpdateDepartment(DepartmentRequest request, ServerCallContext context)
        {
            var dept = await _repository.UpdateDepartamentAsync(new Department { Id = request.DepartmentId, Name = request.DepartmentName });
            DepartmentReply reply = new() { DepartmentId = dept, DepartmentName = request.DepartmentName };
            return reply;
        }

        public override async Task<DepartmentReply> DeleteDepartment(DepartmentRequest request, ServerCallContext context)
        {
            var dept = await _repository.DeleteDepartamentAsync(new Department { Id = request.DepartmentId, Name = request.DepartmentName });
            DepartmentReply reply = new() { DepartmentId = dept, DepartmentName = request.DepartmentName };
            return reply;
        }
    }
}
