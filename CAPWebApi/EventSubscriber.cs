using DotNetCore.CAP;
using System;

namespace CAPWebApi
{
    public class EventSubscriber : ICapSubscribe
    {
        [CapSubscribe("hello")]
        public void Handle(string value)
        {
            Console.WriteLine($"{value} {DateTime.Now}");
        }
    }
}
