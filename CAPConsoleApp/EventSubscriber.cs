using DotNetCore.CAP;
using System;

namespace CAPConsoleApp
{
    public class EventSubscriber : ICapSubscribe
    {
        [CapSubscribe("hello")]
        public void Handle(string value)
        {
            Console.WriteLine(value);
        }
    }
}
