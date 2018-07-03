using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Billing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpointInstance = await EndpointFactory.CreateEndpoint("Billing", (routing) =>
            {
                routing.RegisterPublisher(typeof(OrderPlaced), "Sales");
            });

            Console.WriteLine("Press Enter to exit");

            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
