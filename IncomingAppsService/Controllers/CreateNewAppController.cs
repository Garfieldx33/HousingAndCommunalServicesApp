using CommonLib.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace IncomingAppsService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CreateNewAppController : ControllerBase
    {
        RabbitMqPublisherService publisherService;
        public CreateNewAppController(RabbitMqPublisherService rabbitMqPublisherService)
        {
            publisherService = rabbitMqPublisherService;
        }

        [HttpPost(Name = "PostNew")]
        public IActionResult AddNewApp(string subject, string fullText, int userId, int appTypeId, int departamentId, CancellationToken cancellationToken = default)
        {
            try
            {
                ApplicationDTO applicationDTO = new ApplicationDTO
                {
                    ApplicantId = userId,
                    ApplicationTypeId = appTypeId,
                    DateCreate = DateTime.Now,
                    Description = fullText,
                    Subject = subject,
                    DepartamentId = departamentId
                };
                var sendSuccess = publisherService.SendNewApplication(applicationDTO);
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
}
