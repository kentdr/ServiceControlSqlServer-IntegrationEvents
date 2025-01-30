using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NServiceBus;
using NServiceBus.Logging;
using ServiceControl.Contracts;

namespace SCIntegrationEvents
{
    public class HeartbeatHandler : IHandleMessages<HeartbeatStopped>, IHandleMessages<HeartbeatRestored>
    {
        static ILog logger = LogManager.GetLogger<HeartbeatHandler>();

        public Task Handle(HeartbeatStopped message, IMessageHandlerContext context)
        {
            logger.Info($"Heartbeat stopped.");

            return Task.CompletedTask;
        }

        public Task Handle(HeartbeatRestored message, IMessageHandlerContext context)
        {
            logger.Info($"Heartbeat restored.");

            return Task.CompletedTask;
        }
    }
}