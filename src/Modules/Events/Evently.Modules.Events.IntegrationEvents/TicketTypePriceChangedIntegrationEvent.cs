using Evently.Common.Application.EventBus;

namespace Evently.Modules.Events.IntegrationEvents;

public sealed class TicketTypePriceChangedIntegrationEvent : IntegrationEvent
{
    public TicketTypePriceChangedIntegrationEvent(Guid id, DateTime occurredOnUtc, Guid ticketTypeId, decimal price)
        : base(id, occurredOnUtc)
    {
        TicketTypeId = ticketTypeId;
        Price = price;
    }

    public Guid TicketTypeId { get; init; }

    public decimal Price { get; init; }
}
