using CommonLib.DTO;
using CommonWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommonWebService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ApplicationsController : ControllerBase
    {
        AppServiceGrpc _appService;
        public ApplicationsController(AppServiceGrpc appService)
        {
            _appService = appService;
        }

        [HttpGet]
        public async Task<string> GetAppByApplicantId(int applicantId)
        {
            return await _appService.GetAppAsync(applicantId);
        }

        /*[HttpPost]
        public async Task<string> AddApplication([FromQuery] AddNewAppRequest newApp)
        {
            return await _appService.AddAppAsync(newApp);
        }*/

        [HttpPatch]
        public async Task<string> UpdateApplication([FromQuery] UpdateAppDto updatingApp)
        {
            return await _appService.UpdateAppAsync(updatingApp);
        }

        [HttpDelete]
        public async Task<string> DeleteApplication([FromQuery] DeleteAppRequest deletingApp)
        {
            return await _appService.DeleteAppAsync(deletingApp);
        }
    }
}
