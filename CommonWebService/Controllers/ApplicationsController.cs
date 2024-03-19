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

    [HttpGet("{applicantId}")]
    public async Task<ActionResult<List<Application>>> GetAppByApplicantId(int applicantId)
    {
        var result =  await _appService.GetAppsByUserIdAsync(applicantId);
        if (result != null) 
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("{departmentId}")]
    public async Task<ActionResult<List<Application>>> GetOpenedAppByDepartmentId(int departmentId)
    {
        var result = await _appService.GetOpenedAppsByDepartmentIdAsync(departmentId);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("{executorId}")]
    public async Task<ActionResult<List<Application>>> GetAppByExecutorId(int executorId)
    {
        var result = await _appService.GetAppsByExecutorIdAsync(executorId);
        if (result != null)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPatch]
    public async Task<ActionResult<Application>> UpdateApplication([FromBody] UpdateAppDTO updatingApp)
    {
        var resut  = await _appService.UpdateAppAsync(updatingApp);
        return Ok(resut);
    }

    [HttpDelete("{deletingAppId}")]
    public async Task<ActionResult<string>> DeleteApplication(int deletingAppId)
    {
        var result = await _appService.DeleteAppAsync(new DeleteAppRequest { DeleteAppId = deletingAppId });
        return Ok(result.Item2);
    }
}
