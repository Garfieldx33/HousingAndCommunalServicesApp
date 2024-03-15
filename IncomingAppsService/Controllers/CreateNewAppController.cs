using CommonLib.DTO;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace IncomingAppsService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CreateNewAppController : ControllerBase
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly RabbitMqPublisherService _publisherService;
    public CreateNewAppController(RabbitMqPublisherService rabbitMqPublisherService)
    {
        _publisherService = rabbitMqPublisherService;
    }

    [HttpPost()]
    public IActionResult AddNewApp(ApplicationDTO newApplication, CancellationToken cancellationToken = default)
    {
        var sendSuccess = _publisherService.SendNewApplication(newApplication);
        _logger.Debug("Result of adding " + sendSuccess);
        if (sendSuccess)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}
