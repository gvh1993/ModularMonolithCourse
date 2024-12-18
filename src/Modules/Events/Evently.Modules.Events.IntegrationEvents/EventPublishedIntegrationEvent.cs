using Evently.Common.Application.EventBus;

namespace Evently.Modules.Events.IntegrationEvents;

public sealed class EventPublishedIntegrationEvent : IntegrationEvent
{
    public EventPublishedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid eventId,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc,
        List<TicketTypeModel> ticketTypes)
        : base(id, occurredOnUtc)
    {
        EventId = eventId;
        Title = title;
        Description = description;
        Location = location;
        StartsAtUtc = startsAtUtc;
        EndsAtUtc = endsAtUtc;
        TicketTypes = ticketTypes;
    }

    public Guid EventId { get; init; }

    public string Title { get; init; }

    public string Description { get; init; }

    public string Location { get; init; }

    public DateTime StartsAtUtc { get; init; }

    public DateTime? EndsAtUtc { get; init; }

    public List<TicketTypeModel> TicketTypes { get; init; }
}
