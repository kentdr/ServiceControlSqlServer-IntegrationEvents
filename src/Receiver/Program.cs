using System;
using NServiceBus;


Console.Title = "SimpleReceiver";
var endpointConfiguration = new EndpointConfiguration("Samples.SqlServer.SimpleReceiver");

// for SqlExpress use Data Source=.\SqlExpress;Initial Catalog=SqlServerSimple;Integrated Security=True;Max Pool Size=100;Encrypt=false
var connectionString = @"Server=localhost,1433;Initial Catalog=sc-integration-tests;User Id=SA;Password=yourStrong(!)Password;Max Pool Size=100;Encrypt=false";

endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.UseTransport(new SqlServerTransport(connectionString)
{
    TransportTransactionMode = TransportTransactionMode.SendsAtomicWithReceive
});

endpointConfiguration.SendHeartbeatTo("Particular.sc-integration-events-test", TimeSpan.FromSeconds(1));

endpointConfiguration.EnableInstallers();

await SqlHelper.EnsureDatabaseExists(connectionString);
var endpointInstance = await Endpoint.Start(endpointConfiguration);
Console.WriteLine("Press any key to exit");
Console.WriteLine("Waiting for message from the Sender");
Console.ReadKey();
await endpointInstance.Stop();