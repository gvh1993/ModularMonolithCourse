using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Events;

public sealed class EventPublishedDomainEvent(Guid eventId) : DomainEvent
{
    public Guid EventId { get; init; } = eventId;
}
