using Evently.Common.Application.EventBus;

namespace Evently.Modules.Ticketing.IntegrationEvents;

public sealed class TicketIssuedIntegrationEvent : IntegrationEvent
{
    public TicketIssuedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid ticketId,
        Guid customerId,
        Guid eventId,
        string code)
        : base(id, occurredOnUtc)
    {
        TicketId = ticketId;
        CustomerId = customerId;
        EventId = eventId;
        Code = code;
    }

    public Guid TicketId { get; init; }

    public Guid CustomerId { get; init; }

    public Guid EventId { get; init; }

    public string Code { get; init; }
}
