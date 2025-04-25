
using RabbitMQ.Client;
using SenderRabbitMQ.Send;
using System.Text;
using System.Text.Json;




ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };
IConnection connection = await factory.CreateConnectionAsync();
IChannel channel = await connection.CreateChannelAsync();


await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false,
    arguments: null);

//await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

var message = GetMessage(args);

User user = new User()
{
    nome = "Rubens"
};

var userJson = JsonSerializer.Serialize(user);

var body = Encoding.UTF8.GetBytes(userJson);
//var body = Encoding.UTF8.GetBytes(message);

var properties = new BasicProperties
{
    Persistent = true
};

await channel.BasicPublishAsync(exchange: "", routingKey: "hello", body: body);

Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();


static string GetMessage(string[] args)
{
    return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
}

public class User
{
    public string? nome { get; set; }
}