using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Shipping
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var endpointInstance = await EndpointFactory.CreateEndpoint("Shipping", (routing) =>
            {
                routing.RegisterPublisher(typeof(OrderPlaced), "Sales");
                routing.RegisterPublisher(typeof(OrderPlaced), "Billing");
            });

            Console.WriteLine("Press Enter to exit");

            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
