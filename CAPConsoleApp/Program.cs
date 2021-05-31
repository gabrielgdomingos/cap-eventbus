using DotNetCore.CAP.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace CAPConsoleApp
{
    public static class Program
    {
        public static void Main()
        {
            var container = new ServiceCollection();

            container.AddLogging(x => x.AddConsole());

            container.AddCap(x =>
            {
                x.UseInMemoryStorage();

                x.DefaultGroupName = $"cap.queue.{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}";
                x.Version = "v1";
                x.FailedRetryInterval = 60;
                x.ConsumerThreadCount = 1;
                x.FailedRetryCount = 50;

                x.UseRabbitMQ(x =>
                {
                    x.HostName = "localhost";
                    x.VirtualHost = "CAP";
                    x.Port = 5672;
                    x.UserName = "admin";
                    x.Password = "admin";
                    x.ExchangeName = "cap.default.router";
                });
            });

            container.AddSingleton<EventSubscriber>();

            var sp = container.BuildServiceProvider();

            sp.GetService<IBootstrapper>().BootstrapAsync(default);

            Console.ReadLine();
        }
    }
}
