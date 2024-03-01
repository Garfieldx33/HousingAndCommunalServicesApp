using CommonLib.Config;
using CommonLib.DTO;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using IModel = RabbitMQ.Client.IModel;

namespace DataAccessGrpcService.Services
{
    //Сервис отправки уведомлений в очередь
    public class NotificationQueueService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        RabbitMqConfig _rabbitConfig;
        public IConnection Connection { get; set; }
        public IModel Channel { get; set; }

        public NotificationQueueService(IOptions<RabbitMqConfig> rabbitConfig)
        {
            _rabbitConfig = rabbitConfig.Value;
        }

        private IConnection CreateConnection()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                AutomaticRecoveryEnabled = _rabbitConfig.RetryCount != 0 ? true : false,
                HostName = _rabbitConfig.Host,
                Port = _rabbitConfig.Port,
                UserName = _rabbitConfig.Username,
                Password = _rabbitConfig.Password
            };
            if (_rabbitConfig.RetryCount != 0)
            {
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(_rabbitConfig.ReconnectInterval);
            }
            if (_rabbitConfig.RetryCount < 0)
            {
                factory.RequestedConnectionTimeout = TimeSpan.FromDays(2);
            }
            else if (_rabbitConfig.RetryCount > 0)
            {
                factory.RequestedConnectionTimeout = TimeSpan.FromSeconds(_rabbitConfig.ReconnectInterval * _rabbitConfig.RetryCount);
            }
            IConnection rabbitConnection = factory.CreateConnection();
            var prop = rabbitConnection.ServerProperties;
            rabbitConnection.ConnectionShutdown += ConnectionShutdown;
            rabbitConnection.ConnectionBlocked += ConnectionBlocked;
            _logger.Info(nameof(RabbitMqPublisherService) + " Соединение с RabbitMQ установлено успешно");
            return rabbitConnection;
        }

        private IModel? CreateChannel(IConnection connection)
        {
            if (connection == null)
            {
                return null;
            }
            try
            {
                Channel = connection.CreateModel();
                _logger.Debug(nameof(NotificationQueueService) + $" канал создан успешно");
                return Channel;
            }
            catch (Exception ex)
            {
                _logger.Error(nameof(NotificationQueueService) + $" при создании канала произошла ошибка: {ex.Message}");
                return null;
            }
        }

        private bool PublishMessage(MessageDTO newMessage, IModel channel)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newMessage));
                IBasicProperties prop = channel.CreateBasicProperties();
                prop.ContentType = "application/json";
                prop.Persistent = true;

                channel.BasicPublish(exchange: _rabbitConfig.ExchangeName,
                              routingKey: _rabbitConfig.RoutingKey,
                              basicProperties: prop,
                              body: body);
                return true;
            }
            catch (Exception e)
            {
                _logger.Warn($"Не удалось выполнить отправку уведомления. {e.Message}");
                return false;
            }
        }
        public bool SendNewMessage(MessageDTO newMessage)
        {
            var connect = CreateConnection();
            var channel = CreateChannel(connect);
            return PublishMessage(newMessage, channel);
        }

        private void ConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            _logger.Warn($"Соединение с Rabbit блокировано {e.Reason}");
        }

        private void ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            _logger.Warn($"Соединение с Rabbit потеряно {e.ReplyText} Инициатор {e.Initiator} {e.ReplyCode}");
            if (_rabbitConfig.RetryCount == 0)
            {
                _logger.Warn("Функция восcтановления соеднинения с брокером сообщений целевой очереди отключена.");
            }
        }

    }
}
