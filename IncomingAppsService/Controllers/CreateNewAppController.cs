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
    public string AddNewApp([FromBody] ApplicationDTO newApplication, CancellationToken cancellationToken = default)
    {
        try
        {
            var sendSuccess = _publisherService.SendNewApplication(newApplication);
            if (sendSuccess)
            {
                return "�����������! ���� ������ ������� ����������������";
            }
            else
            {
                return "��������� ������ ��� �������� ������ �� ���������, ��������� �����";
            }
        }
        catch (Exception ex)
        {
            _logger.Warn(ex);
            return ex.Message;
        }
    }
}
