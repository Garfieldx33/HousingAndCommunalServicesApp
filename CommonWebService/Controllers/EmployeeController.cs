using CommonLib.Entities;
using CommonWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommonWebService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class EmployeeController
    {
        private readonly EmployeeServiceGrpc _employeeService;

        public EmployeeController(EmployeeServiceGrpc employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<List<User>> GetEmployersByDepartmentId(int departmentId)
        {
            return await _employeeService.GetEmployersByDepartmentId(departmentId);
        }
        
        [HttpGet]
        public async Task<string> GetDepartmentByUserId(int userId)
        {
            return await _employeeService.GetDepartmentByUserId(userId);
        }

        [HttpPost]
        public async Task<string> AddEmployee([FromBody] EmployeeInfo newEmployee)
        {
           return await _employeeService.AddEmployee(newEmployee);
        }

        [HttpPatch]
        public async Task<string> UpdateEmployee([FromBody] EmployeeInfo employeeInfo)
        {
            return await _employeeService.UpdateEmployee(employeeInfo);
        }

        [HttpDelete]
        public async Task<string> DeleteEmployee(int employeeId)
        {
            return await _employeeService.DeleteEmployee(employeeId);
        }
    }
}
