using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Events;

public sealed class EventPaymentsRefundedDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
