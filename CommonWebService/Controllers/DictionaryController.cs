using CommonLib.Entities;
using CommonWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommonWebService.Controllers;

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
    public async Task<Department> GetDepartmentById(int departmentId)
    {
        return await _dictionaryService.GetDepartmentByIdAsync(departmentId);
    }

    [HttpGet]
    public async Task<List<Department>> GetAllDepartments()
    {
        return await _dictionaryService.GetAllDepartmentsAsync();
    }
    
    [HttpPost]
    public async Task<Department> AddDepartment(string departmentName)
    {
        return await _dictionaryService.AddDepartmentAsync(departmentName);
    }
    
    [HttpPatch]
    public async Task<string> UpdateDepartment([FromBody] Department department)
    {
        return await _dictionaryService.UpdateDepartmentAsync(department);
    }

    [HttpDelete]
    public async Task<Department> DeleteDepartment([FromBody] Department department)
    {
        return await _dictionaryService.DeleteDepartmentAsync(department);
    }

    [HttpGet]
    public async Task<Dictionary<int,string>> GetAppStatuses()
    {
        return await _dictionaryService.GetAppStatusesAsync();
    }

    [HttpGet]
    public async Task<Dictionary<int, string>> GetAppTypes()
    {
        return await _dictionaryService.GetAppTypesAsync();
    }

    [HttpGet]
    public async Task<Dictionary<int, string>> GetUserTypes()
    {
        return await _dictionaryService.GetAppTypesAsync();
    }
}
