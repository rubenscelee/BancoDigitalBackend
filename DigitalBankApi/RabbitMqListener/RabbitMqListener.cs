using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using System.Threading.Channels;

namespace DigitalBankApi.RabbitMqListener
{
    public class RabbitMqListener : BackgroundService
    {
        private ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

        public RabbitMqListener()
        {
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IConnection? connection = await factory.CreateConnectionAsync();

            var channel = connection.CreateChannelAsync();

            //await channel.QueueDeclareAsync(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);

            //var consumer = new EventingBasicConsumer(channel);
            //consumer.Received += (model, ea) =>
            //{
            //    var body = ea.Body.ToArray();
            //    var message = Encoding.UTF8.GetString(body);
            //    Console.WriteLine($"Received: {message}");
            //};

            //await channel.BasicConsume(queue: "myQueue", autoAck: true, consumer: consumer);
            //return Task.CompletedTask;
        }
    }
}
