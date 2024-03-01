using CommonLib.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NotificationService
{
    public class IncomingNotificationWorker : IHostedService, IDisposable
    {
        Logger _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        RabbitMqConfig _rabbitMqConfig;
        IMapper _mapper;
        public IConnection? _rabbitConnection { get; set; }
        public IModel? _channel { get; set; }

        public IncomingNotificationWorker(IOptions<RabbitMqConfig> rabbitConfig, IMapper mapper)
        {
            _rabbitMqConfig = rabbitConfig.Value;
            _mapper = mapper;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}