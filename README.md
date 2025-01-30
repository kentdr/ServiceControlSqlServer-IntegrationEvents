# Service Control Integration Events

Here is a working solution that handles HearbeatStopped and HeartbeatRestored events from ServiceControl.

This solution uses SqlTransport version 7.0.9 and .NET Framework 4.8.

To run the solution:
  - Update the connection strings in the Sender, Receiver, and SCIntegrationEvents projects to match a local or test SQL Server instance
  - Update the Hearbeat queue to match the name of your test ServiceControl error instance
  - Run the Sender, Receiver, and SCIntegrationEvents projects

To test the solution:
  Once all the projects are running and you see Healthy endpoints in ServicePulse, close either Sender or Receiver and wait for SCIntegrationEvents to log that the Heartbeat has stopped. Once received, start the previously stopped project again and wait for the Heartbeat restored log entry.
