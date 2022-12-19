using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.UserName = "guest";
            factory.Password = "guest";
            factory.VirtualHost = "/";
            factory.Port = 5672;

            using (var connection = factory.CreateConnection())
            {                
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("test", true, false, false);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (ch, ea) =>
                    {
                        var message = ea.Body.ToString();
                        channel.BasicAck(ea.DeliveryTag, false);
                        Console.WriteLine("Message recieved: ", message);
                    };

                    while (true)
                    {
                        channel.BasicConsume("test", false, consumer);
                        System.Threading.Thread.Sleep(2000);
                    }
                    //No gentle channel/connection close for the consumer
                }
            }
        }
    }
}
