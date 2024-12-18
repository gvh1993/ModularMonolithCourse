using Evently.Common.Application.EventBus;

namespace Evently.Modules.Ticketing.IntegrationEvents;

public sealed class TicketArchivedIntegrationEvent : IntegrationEvent
{
    public TicketArchivedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid ticketId,
        string code)
        : base(id, occurredOnUtc)
    {
        TicketId = ticketId;
        Code = code;
    }

    public Guid TicketId { get; init; }

    public string Code { get; init; }
}
