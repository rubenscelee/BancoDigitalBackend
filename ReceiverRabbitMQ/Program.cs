using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
    arguments: null);

//await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new AsyncEventingBasicConsumer(channel);

consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($" [x] Received {message}\n");

    int dots = message.Split('.').Length - 1;
    await Task.Delay(dots * 1000);

    Console.WriteLine(" [x] Done");

    // here channel could also be accessed as ((AsyncEventingBasicConsumer)sender).Channel
    await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
};

//autoAck = false faz com que a mensagem não seja perdida caso o consumer cancele a operação por algum motivo. Assim que um consumer estiver habilitado, irá consumir
//a mensagem pendente, até finalizá-la
await channel.BasicConsumeAsync("hello", autoAck: false, consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

//consumer.ReceivedAsync += (model, ea) =>
//{
//    var body = ea.Body.ToArray();
//    var message = Encoding.UTF8.GetString(body);
//    Console.WriteLine($" [x] Received {message}");
//    return Task.CompletedTask;
//};

//await channel.BasicConsumeAsync("hello", autoAck: true, consumer: consumer);

//Console.WriteLine(" Press [enter] to exit.");
//Console.ReadLine();

public class User
{
    public string? nome { get; set; }
}