using CommonLib.DTO;
using CommonWebService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommonWebService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ApplicationsController : ControllerBase
{
    private readonly AppServiceGrpc _appService;
    public ApplicationsController(AppServiceGrpc appService)
    {
        _appService = appService;
    }

    [HttpGet]
    public async Task<string> GetAppByApplicantId(int applicantId)
    {
        return await _appService.GetAppAsync(applicantId);
    }

    [HttpPatch]
    public async Task<string> UpdateApplication([FromQuery] UpdateAppDTO updatingApp)
    {
        return await _appService.UpdateAppAsync(updatingApp);
    }

    [HttpDelete]
    public async Task<string> DeleteApplication([FromQuery] DeleteAppRequest deletingApp)
    {
        return await _appService.DeleteAppAsync(deletingApp);
    }
}
