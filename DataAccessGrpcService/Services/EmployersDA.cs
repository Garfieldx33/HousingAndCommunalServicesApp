using CommonLib.Entities;
using Grpc.Core;

namespace DataAccessGrpcService.Services
{
    public partial class DataAccessGrpcBase
    {
        //EmployersInfo
        //Create 
        public override async Task<EmployeeOperationResultInfo> AddEmployee(EmployeeInfoGrpc newEmployee, ServerCallContext context)
        {
            try
            {
                var newEmpl = _mapper.Map<EmployeeInfo>(newEmployee);
                var id = await _repository.AddNewEmployeeAsync(newEmpl);
                string addingResult = id > 0 ? "Работник успешно добавлен" : "Ошибка при добавлении работника";
                return new EmployeeOperationResultInfo { OperationResult = addingResult };
            }
           catch (Exception ex) 
            {
                return new EmployeeOperationResultInfo { OperationResult = $"Ошибка выполнения операции добавления работника. {ex.Message}"};
            }
            
        }

        //Read
        public override async Task<UsersReply> GetEmployersInfoByDepartmentId(EmployeeInfoRequest request, ServerCallContext context)
        {
            UsersReply result = new();
            List<User> usersList = await _repository.GetUsersByDepartmentIdAsync(request.SearchingId);
            usersList.ForEach(u => result.Users.Add(_mapper.Map<UserGrpc>(u)));
            return result;
        }

        public override async Task<DepartmentReply> GetDepartmentByUserId(EmployeeInfoRequest request, ServerCallContext context)
        {
            string searchResult = await _repository.GetDepartmentByUserIdAsync(request.SearchingId);
            return new DepartmentReply { DepartmentId = 0, DepartmentName = searchResult };
        }

        //Update
        public override async Task<EmployeeOperationResultInfo> UpdateEmployee(EmployeeInfoGrpc updateEmplInfo, ServerCallContext context)
        {
            EmployeeOperationResultInfo result = new();
            var updtEmplInfo = _mapper.Map<EmployeeInfo>(updateEmplInfo);
            var res = await _repository.UpdateEmployeeAsync(updtEmplInfo);
            if (res > 0)
            {
                result.OperationResult = $"Успешное обновление информации о работнике c Id {updtEmplInfo.EmployeeUserId}";
            }
            else
            {
                result.OperationResult = $"Не удалось обновить информацию о работнике с Id {updtEmplInfo.EmployeeUserId}";
            }
            return result;
        }

        //Delete
        public override async Task<EmployeeOperationResultInfo> DeleteEmployee(EmployeeInfoRequest request, ServerCallContext context)
        {
            EmployeeOperationResultInfo resultInfo = new();
            int deleteResultId = await _repository.DeleteEmployeeAsync(request.SearchingId);
            if (deleteResultId > 0)
            {
                resultInfo.OperationResult = $"Успешное удаление работника c Id {request.SearchingId}";
            }
            else
            {
                resultInfo.OperationResult = $"Не удалось удалить работника с Id {request.SearchingId}";
            }
            return resultInfo;
        }

    }
}
