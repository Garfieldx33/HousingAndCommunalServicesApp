using CommonLib.Entities;
using CommonWebService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CommonWebService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DictionaryController : ControllerBase
    {
        DictionaryServiceGrpc _dictionaryService;
        public DictionaryController(DictionaryServiceGrpc dictionaryGrpcService)
        {
            _dictionaryService = dictionaryGrpcService;
        }

        [HttpGet]
        public async Task<string> GetDepartmentById(int departmentId)
        {
            return await _dictionaryService.GetDepartmentByIdAsync(departmentId);
        }

        [HttpGet]
        public async Task<string> GetAllDepartments()
        {
            return await _dictionaryService.GetAllDepartmentsAsync();
        }
        
        [HttpPost]
        public async Task<string> AddDepartment(string departmentName)
        {
            return await _dictionaryService.AddDepartmentAsync(departmentName);
        }
        
        [HttpPatch]
        public async Task<string> UpdateDepartment(Department department)
        {
            return await _dictionaryService.UpdateDepartmentAsync(department);
        }

        [HttpDelete]
        public async Task<string> DeleteDepartment(Department department)
        {
            return await _dictionaryService.DeleteDepartmentAsync(department);
        }

        [HttpGet]
        public async Task<string> GetAppStatuses()
        {
            return await _dictionaryService.GetAppStatusesAsync();
        }

        [HttpGet]
        public async Task<string> GetAppTypes()
        {
            return await _dictionaryService.GetAppTypesAsync();
        }

        [HttpGet]
        public async Task<string> GetUserTypes()
        {
            return await _dictionaryService.GetAppTypesAsync();
        }

    }
}
