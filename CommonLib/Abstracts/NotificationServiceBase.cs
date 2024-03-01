using AutoMapper;
using CommonLib.Config;
using CommonLib.DTO;
using CommonLib.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using IModel = RabbitMQ.Client.IModel;

namespace CommonLib.Abstracts
{
    public class NotificationServiceBase : INotificationService
    {
        readonly RabbitMqConfig _incomingExchageConfig;
        readonly IMapper _mapper;
        public IConnection? RabbitConnection { get; set; }
        public IModel? Channel { get; set; }
        public NotificationServiceBase(RabbitMqConfig rabbitMqConfig, IMapper mapper) 
        {
            _incomingExchageConfig = rabbitMqConfig;
            _mapper = mapper;
        }

        public MessageDTO GetMessageFromQueue()
        {
            throw new NotImplementedException();
        }

        public virtual void SendMessage(MessageDTO message)
        {
            throw new NotImplementedException();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ConnectionFactory factory = CreateConnectionFactory(_incomingExchageConfig);
            RabbitConnection = CreateRabbitMqConnection(factory);
            if (RabbitConnection is null)
            {
                throw new Exception("Невозможно создать соединение с брокером сообщений");
            }
            Channel = RabbitConnection.CreateModel();
            if (RabbitConnection is null)
            {
                throw new Exception("Невозможно создать канал с брокером сообщений");
            }
            CreateConsumer(Channel, _incomingExchageConfig.QueueName, ReceivedNewAppData);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            return Task.CompletedTask;
        }

        private ConnectionFactory CreateConnectionFactory(RabbitMqConfig options)
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                AutomaticRecoveryEnabled = _incomingExchageConfig.RetryCount != 0 ? true : false,
                HostName = _incomingExchageConfig.Host,
                Port = _incomingExchageConfig.Port,
                UserName = _incomingExchageConfig.Username,
                Password = _incomingExchageConfig.Password
            };
            if (_incomingExchageConfig.RetryCount != 0)
            {
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(_incomingExchageConfig.ReconnectInterval);
            }
            if (_incomingExchageConfig.RetryCount < 0)
            {
                factory.RequestedConnectionTimeout = TimeSpan.FromDays(2);
            }
            else if (_incomingExchageConfig.RetryCount > 0)
            {
                factory.RequestedConnectionTimeout = TimeSpan.FromSeconds(_incomingExchageConfig.ReconnectInterval * _incomingExchageConfig.RetryCount);
            }
            return factory;
        }

        private IConnection? CreateRabbitMqConnection(ConnectionFactory factory)
        {
            if (factory is not null)
            {
                IConnection rabbitConnection = factory.CreateConnection();
                var prop = rabbitConnection.ServerProperties;
                return rabbitConnection;
            }
            else
            {
                return null;
            }
        }

        private EventingBasicConsumer CreateConsumer(IModel channel, string queueName, Action<object, BasicDeliverEventArgs> action)
        {
            if (action is null)
            {
                throw new Exception("Требуется передать действие на событие нового сообщения от RabbitMQ");
            }
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += new EventHandler<BasicDeliverEventArgs>(action);
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            return consumer;
        }

        private void ReceivedNewAppData(object? sender, BasicDeliverEventArgs e)
        {
            if (Channel is null)
            {
                throw new Exception("Канал закрыт, работа сервиса остановлена");
            }
            var message = JsonConvert.DeserializeObject<MessageDTO>(Encoding.UTF8.GetString(e.Body.ToArray()));
            SendMessage(message);
            Channel.BasicAck(e.DeliveryTag, false);
        }

        public void Dispose()
        {
            if (Channel is not null)
            {
                Channel.Close();
                Channel.Dispose();
            }
            if (RabbitConnection is not null)
            {
                RabbitConnection.Close();
                RabbitConnection.Dispose();
            }
        }

    }
}
