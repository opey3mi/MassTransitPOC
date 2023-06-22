using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassTransitPOC
{
    public class MyMessageProducer
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MyMessageProducer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendMessage(string text)
        {
            await _publishEndpoint.Publish(new MyMessage { Text = text });
        }
    }
}
