using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Events;

public static class EventErrors
{
    public static Error NotFound(Guid eventId) =>
        Error.NotFound("Events.NotFound", $"The event with the identifier {eventId} was not found");

    public static readonly Error StartDateInPast = Error.Problem(
        "Events.StartDateInPast",
        "The event start date is in the past");
}
