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
        public IActionResult AddNewApp([FromBody] ApplicationDTO newApplication, CancellationToken cancellationToken = default)
        {
            try
            {
                var sendSuccess = publisherService.SendNewApplication(newApplication);
                if (sendSuccess)
                {
                    return Ok("�����������! ���� ������ ������� ����������������");
                }
                else
                {
                    return BadRequest("��������� ������ ��� �������� ������ �� ���������, ��������� �����");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
