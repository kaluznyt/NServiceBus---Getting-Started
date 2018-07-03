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

        private static async Task<IEndpointInstance> CreateEndpoint(string endpointName)
        {
            Console.Title = endpointName;

            var endpointConfiguration = new EndpointConfiguration(endpointName);

            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            return endpointInstance;
        }
    }
}
