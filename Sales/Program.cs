using Messages;

namespace Sales
{
    using System;
    using System.Threading.Tasks;

    using NServiceBus;

    class Program
    {
        static async Task Main(string[] args)
        {
            var endpointInstance = await EndpointFactory.CreateEndpoint("Sales",
                recoverabilitySettings: rs => rs.Delayed(delayed => delayed.NumberOfRetries(0)));

            Console.WriteLine("Press Enter to exit");

            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
