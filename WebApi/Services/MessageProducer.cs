using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace WebApi.Services;

public class MessageProducer : IMessageProducer
{
    public void SendMessaging<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password = "password",
            VirtualHost = "/",
        };

        var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "bookingQueue",
                             durable: true,
                             exclusive: true);

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish(exchange: "",
                             routingKey: "bookingQueue",
                             basicProperties: null,
                             body: body);
    }
}
