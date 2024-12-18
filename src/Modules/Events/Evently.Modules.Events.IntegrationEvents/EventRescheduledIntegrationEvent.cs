using Evently.Common.Application.EventBus;

namespace Evently.Modules.Events.IntegrationEvents;

public sealed class EventRescheduledIntegrationEvent : IntegrationEvent
{
    public EventRescheduledIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid eventId,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
        : base(id, occurredOnUtc)
    {
        EventId = eventId;
        StartsAtUtc = startsAtUtc;
        EndsAtUtc = endsAtUtc;
    }

    public Guid EventId { get; init; }

    public DateTime StartsAtUtc { get; init; }

    public DateTime? EndsAtUtc { get; init; }
}
