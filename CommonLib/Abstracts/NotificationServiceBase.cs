﻿using CommonLib.Config;
using CommonLib.DTO;
using CommonLib.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using IModel = RabbitMQ.Client.IModel;

namespace CommonLib.Abstracts;

public abstract class NotificationServiceBase : INotificationService, IDisposable
{
    private readonly RabbitMqConfig _incomingExchageConfig;
    private IConnection? RabbitConnection;
    private IModel? Channel;
    private EventingBasicConsumer _consumer;
    object _messageLocker = new();
    public NotificationServiceBase(IOptions<RabbitMqConfig> rabbitConfig)
    {
        _incomingExchageConfig = rabbitConfig.Value;
    }

    public abstract bool SendMessage(MessageDTO message);

    protected virtual Task StartAsync(CancellationToken cancellationToken)
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
        _consumer = CreateConsumer(Channel, _incomingExchageConfig.QueueName, ReceivedNewAppData);
        return Task.CompletedTask;
    }

    protected virtual Task StopAsync(CancellationToken cancellationToken)
    {
        Dispose();
        return Task.CompletedTask;
    }

    public virtual void Dispose()
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

    protected ConnectionFactory CreateConnectionFactory(RabbitMqConfig options)
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

    protected IConnection? CreateRabbitMqConnection(ConnectionFactory factory)
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

    protected EventingBasicConsumer CreateConsumer(IModel channel, string queueName, Action<object, BasicDeliverEventArgs> action)
    {
        if (action is null)
        {
            throw new Exception("Требуется передать действие на событие нового сообщения от RabbitMQ");
        }
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += new EventHandler<BasicDeliverEventArgs>(action!);
        channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
        return consumer;
    }

    protected void ReceivedNewAppData(object? sender, BasicDeliverEventArgs e)
    {
        Monitor.Enter(_messageLocker);
        if (Channel is null)
        {
            throw new Exception("Канал закрыт, работа сервиса остановлена");
        }
        var message = JsonConvert.DeserializeObject<MessageDTO>(Encoding.UTF8.GetString(e.Body.ToArray()));
        bool sendSuccess = SendMessage(message);
        Thread.Sleep(10000); // Имитация долгой отправки
        if (sendSuccess)
        {
            Channel.BasicAck(e.DeliveryTag, false);
        }
        
        Monitor.Exit(_messageLocker);
    }
}
