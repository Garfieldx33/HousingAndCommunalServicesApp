using CommonGrpcService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommonGrpcService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DictionaryController : ControllerBase
    {
        DictionaryGrpcService _dictionaryService;
        public DictionaryController(DictionaryGrpcService dictionaryGrpcService) 
        {
            _dictionaryService = dictionaryGrpcService;
        }

        [HttpGet]
        public async Task<string> GetDepartmentById(int departmentId)
        {
            return _dictionaryService.GetDepartmentById(departmentId);
        }
    }
}
