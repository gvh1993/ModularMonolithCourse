using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Events;

public sealed class Event : Entity
{
    private Event()
    {
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public string Location { get; private set; }

    public DateTime StartsAtUtc { get; private set; }

    public DateTime? EndsAtUtc { get; private set; }

    public static Event Create(
        Guid id,
        string title,
        string description,
        string location,
        DateTime startsAtUtc,
        DateTime? endsAtUtc)
    {
        var @event = new Event
        {
            Id = id,
            Title = title,
            Description = description,
            Location = location,
            StartsAtUtc = startsAtUtc,
            EndsAtUtc = endsAtUtc
        };

        @event.Raise(new EventCreatedDomainEvent(
            @event.Id,
            @event.Title,
            @event.Description,
            @event.Location,
            @event.StartsAtUtc,
            @event.EndsAtUtc));

        return @event;
    }
}
