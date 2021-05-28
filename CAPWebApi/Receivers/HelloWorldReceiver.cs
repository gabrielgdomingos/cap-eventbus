using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using System;

namespace CAPWebApi.Receivers
{
    public class HelloWorldReceiver : ICapSubscribe
    {
        private readonly ILogger<HelloWorldReceiver> _logger;

        public HelloWorldReceiver(ILogger<HelloWorldReceiver> logger)
        {
            _logger = logger;
        }

        [CapSubscribe("helloWorld")]
        public void Handle(string value)
        {
            _logger.LogInformation($"{value} {DateTime.Now}");
        }
    }
}
