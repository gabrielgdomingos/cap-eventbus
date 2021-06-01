using DotNetCore.CAP;
using System;

namespace CAPWebApi
{
    public class EventSubscriber : ICapSubscribe
    {
        //A mensagem é consumida pelo Binding da fila
        //O CAP faz uma 2º checagem pelo name passado no atributo
        [CapSubscribe("hello")]
        public void Handle(string value)
        {
            //Console.WriteLine("Received: {0}", value);
            //Thread.Sleep(3000);
            Console.WriteLine("Value: {0}", value);
        }
    }
}
