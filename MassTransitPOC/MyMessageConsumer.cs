using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitPOC
{
    public class MyMessageConsumer : IConsumer<MyMessage>
    {
        public Task Consume(ConsumeContext<MyMessage> context)
        {
            var message = context.Message;
            // Process the message
            Console.WriteLine($"Received message: {message.Text}");

            return Task.CompletedTask;
        }
    }
}
