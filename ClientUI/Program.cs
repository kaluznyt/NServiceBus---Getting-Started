using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    using Messages;

    using NServiceBus;
    using NServiceBus.Logging;

    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();

        static async Task Main(string[] args)
        {
            var endpointInstance = await EndpointFactory.CreateEndpoint("ClientUI", 
                                    (routing) => routing.RouteToEndpoint(typeof(PlaceOrder), "Sales"));

            await RunLoop(endpointInstance)
                    .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

            await endpointInstance.Stop()
                    .ConfigureAwait(false);
        }



        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press P to place an order, or Q to quit");

                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        {
                            var command = new PlaceOrder { OrderId = Guid.NewGuid().ToString() };

                            log.Info($"Sending PlaceOrder command, OrderId = {command.OrderId}");

                            await endpointInstance.Send(command).ConfigureAwait(false);

                            break;
                        }
                    case ConsoleKey.Q:
                        return;
                    default:
                        log.Info("Unknown input. Please try again.");
                        break;
                }
            }
        }
    }
}
