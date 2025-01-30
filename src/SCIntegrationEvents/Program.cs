using System;
using NServiceBus;

Console.Title = "Notifications";
var endpointConfiguration = new EndpointConfiguration("IntegrationEvents.Notifications");

// for SqlExpress use Data Source=.\SqlExpress;Initial Catalog=SqlServerSimple;Integrated Security=True;Max Pool Size=100;Encrypt=false
var connectionString = @"Server=localhost,1433;Initial Catalog=sc-integration-tests;User Id=SA;Password=yourStrong(!)Password;Max Pool Size=100;Encrypt=false";

endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.UseTransport(new SqlServerTransport(connectionString)
{
    TransportTransactionMode = TransportTransactionMode.SendsAtomicWithReceive
});

endpointConfiguration.EnableInstallers();
var conventions = endpointConfiguration.Conventions();
conventions.DefiningEventsAs(
    type => typeof(IEvent).IsAssignableFrom(type) ||
            // include ServiceControl events
            type.Namespace != null &&
            type.Namespace.StartsWith("ServiceControl.Contracts"));

var endpointInstance = await Endpoint.Start(endpointConfiguration);
Console.WriteLine("Press any key to exit");
Console.WriteLine("Waiting for Heartbeat events");
Console.ReadKey();
await endpointInstance.Stop();