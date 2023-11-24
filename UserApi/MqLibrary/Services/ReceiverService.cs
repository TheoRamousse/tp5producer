using MqLibrary.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MqLibrary.Services
{
    public class ReceiverService : IReceiverService
    {
        private readonly EventingBasicConsumer _consumer;
        private readonly IModel _channel;

        public ReceiverService(string url, string queue, Action<object, BasicDeliverEventArgs> SubscribeMedthod)
        {
            var factory = new ConnectionFactory
            {
                HostName = url
            };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _channel.QueueDeclare(queue, exclusive: false);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += (model, eventArgs) =>
            {
                SubscribeMedthod(model, eventArgs);
            };
        }

        public void Start()
        {
            _channel.BasicConsume(queue: "orders", autoAck: true, consumer: _consumer);
            Console.ReadKey();
            _channel.Close();

        }
    }
}
