using System;
using RabbitMQ.Client;

namespace Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            //Provide messages using args
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
                    var props = channel.CreateBasicProperties();
                    props.ContentType = "text/plain";
                    props.DeliveryMode = 2;

                    foreach (var message in args)
                    {
                        var messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish("", "test", props, messageBodyBytes);
                        Console.WriteLine("Sent message:", message);
                    }

                    channel.Close();
                }
                
                connection.Close();
            }
            Console.WriteLine("Connections closed");
            Console.ReadLine();
        }
    }
}
