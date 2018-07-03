using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace Messages
{
    public static class EndpointFactory
    {
        public static async Task<IEndpointInstance> CreateEndpoint(string endpointName,
            Action<RoutingSettings<MsmqTransport>> routingSettings = null,
            Action<RecoverabilitySettings> recoverabilitySettings = null)
        {
            Console.Title = endpointName;

            var endpointConfiguration = new EndpointConfiguration(endpointName);

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            recoverabilitySettings?.Invoke(endpointConfiguration.Recoverability());

            routingSettings?.Invoke(transport.Routing());

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            return endpointInstance;
        }
    }
}
