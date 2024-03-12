using CommonLib.DTO;
using CommonLib.Entities;
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
    public async Task<List<Application>> GetAppByApplicantId(int applicantId)
    {
        return await _appService.GetAppAsync(applicantId);
    }

    [HttpPatch]
    public async Task<Application> UpdateApplication([FromBody] UpdateAppDTO updatingApp)
    {
        return await _appService.UpdateAppAsync(updatingApp);
    }

    [HttpDelete]
    public async Task<string> DeleteApplication(int deletingAppId)
    {
        var result = await _appService.DeleteAppAsync(new DeleteAppRequest { DeleteAppId = deletingAppId });
        return result.Item2;
    }
}
