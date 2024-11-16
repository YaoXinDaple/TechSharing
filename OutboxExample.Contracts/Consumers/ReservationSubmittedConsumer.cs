using MassTransit;
using Microsoft.Extensions.Logging;
using OutboxExample.Contracts.MessageContracts;

namespace OutboxExample.Contracts.Consumers
{
    public class ReservationSubmittedConsumer : IConsumer<ReservationSubmitted>
    {
        private readonly ILogger<ReservationSubmittedConsumer> logger;

        public ReservationSubmittedConsumer(ILogger<ReservationSubmittedConsumer> logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<ReservationSubmitted> context)
        {
            logger.LogInformation("Reservation submitted: {ReservationId}", context.Message.ReservationId);
            await Task.CompletedTask;
        }
    }
}
