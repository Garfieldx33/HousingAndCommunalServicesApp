using CommonLib.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace IncomingAppsService.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CreateNewAppController : ControllerBase
{
    private readonly RabbitMqPublisherService _publisherService;
    public CreateNewAppController(RabbitMqPublisherService rabbitMqPublisherService)
    {
        _publisherService = rabbitMqPublisherService;
    }

    [HttpPost(Name = "PostNew")]
    public IActionResult AddNewApp([FromBody] ApplicationDTO newApplication, CancellationToken cancellationToken = default)
    {
        try
        {
            var sendSuccess = _publisherService.SendNewApplication(newApplication);
            if (sendSuccess)
            {
                return Ok("Поздравляем! Ваша заявка успешно зарегистрирована");
            }
            else
            {
                return BadRequest("Произошла ошибка при отправке заявки на обработку, повторите позже");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
