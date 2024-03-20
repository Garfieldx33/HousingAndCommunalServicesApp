using AutoMapper;
using CommonLib.Config;
using CommonLib.DTO;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NewAppPrepareService
{
    public class NewAppListener : IHostedService, IDisposable
    {
        Logger _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        RabbitMqConfig _rabbitMqConfig;
        gRpcConfig _gRpcConfig;
        IMapper _mapper;
        public IConnection? _rabbitConnection { get; set; }
        public IModel? _channel { get; set; }

        public NewAppListener(IOptions<RabbitMqConfig> rabbitConfig, IOptions<gRpcConfig> gRpcConfigSection, IMapper mapper)
        {
            _rabbitMqConfig = rabbitConfig.Value;
            _gRpcConfig = gRpcConfigSection.Value;
            _mapper = mapper;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            ConnectionFactory factory = CreateConnectionFactory(_rabbitMqConfig);
            _rabbitConnection = CreateRabbitMqConnection(factory);
            if (_rabbitConnection is null)
            {
                throw new Exception("Невозможно создать соединение с брокером сообщений");
            }
            _channel = _rabbitConnection.CreateModel();
            if (_rabbitConnection is null)
            {
                throw new Exception("Невозможно создать канал с брокером сообщений");
            }
            CreateConsumer(_channel, _rabbitMqConfig.QueueName, ReceivedNewAppData);
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
                AutomaticRecoveryEnabled = _rabbitMqConfig.RetryCount != 0 ? true : false,
                HostName = _rabbitMqConfig.Host,
                Port = _rabbitMqConfig.Port,
                UserName = _rabbitMqConfig.Username,
                Password = _rabbitMqConfig.Password
            };
            if (_rabbitMqConfig.RetryCount != 0)
            {
                factory.NetworkRecoveryInterval = TimeSpan.FromSeconds(_rabbitMqConfig.ReconnectInterval);
            }
            if (_rabbitMqConfig.RetryCount < 0)
            {
                factory.RequestedConnectionTimeout = TimeSpan.FromDays(2);
            }
            else if (_rabbitMqConfig.RetryCount > 0)
            {
                factory.RequestedConnectionTimeout = TimeSpan.FromSeconds(_rabbitMqConfig.ReconnectInterval * _rabbitMqConfig.RetryCount);
            }
            return factory;
        }

        private IConnection? CreateRabbitMqConnection(ConnectionFactory factory)
        {
            if (factory is not null)
            {
                IConnection rabbitConnection = factory.CreateConnection();
                var prop = rabbitConnection.ServerProperties;
                rabbitConnection.ConnectionShutdown += ConnectionShutdown;
                rabbitConnection.ConnectionBlocked += ConnectionBlocked;
                _logger.Info("Соединение с RabbitMQ установлено успешно");
                return rabbitConnection;
            }
            else
            {
                _logger.Error("Фабрика соединений null");
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
            if (_channel is null)
            {
                throw new Exception("Канал закрыт, работа сервиса остановлена");
            }
            var newApp = JsonConvert.DeserializeObject<ApplicationDTO>(Encoding.UTF8.GetString(e.Body.ToArray())); 
            var savingSuccessful = SendNewAppToSave(newApp);
            if (savingSuccessful)
            {
                _channel.BasicAck(e.DeliveryTag, false);
            }
        }

        public void Dispose()
        {
            if (_channel is not null)
            {
                _channel.Close();
                _channel.Dispose();
            }
            if (_rabbitConnection is not null)
            {
                _rabbitConnection.Close();
                _rabbitConnection.Dispose();
            }
        }

        private void ConnectionBlocked(object? sender, ConnectionBlockedEventArgs e)
        {
            _logger.Warn($"Соединение с Rabbit блокировано по причине {e.Reason}");
        }

        private void ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            _logger.Warn($"Соединение с Rabbit разорвано по причине {e.ReplyText}. Инициатор {e.Initiator} {e.ReplyCode}");
            if (_rabbitMqConfig.RetryCount == 0)
            {
                _logger.Warn("Функция восcтановления соеднинения с брокером сообщений целевой очереди отключена.");
            }
        }

        public bool SendNewAppToSave(ApplicationDTO newAppDTO)
        {
            try
            {
                _logger.Debug($"Публикация заявки с пользователя c Id {newAppDTO.ApplicantId} по эндпоинту {_gRpcConfig.HttpsEndpoint}");
                var appGrpcDto = _mapper.Map<ApplicationDtoGrpc>(newAppDTO);
                using var channel = GrpcChannel.ForAddress(_gRpcConfig.HttpsEndpoint);
                var client = new DataAccessGrpcService.DataAccessGrpcServiceClient(channel);
                var reply = client.AddNewApp(new AddNewAppRequest { ApplicationDto = appGrpcDto });
                return reply.ResultOfInsert > 0;
            }
            catch (Exception ex) 
            {
                _logger.Warn($"Не удалось отправить новую заявку на сохранение.Подробнее {ex}");
            }
            return false;
        }
    }
}
