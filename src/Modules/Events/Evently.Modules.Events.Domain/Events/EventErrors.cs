using Evently.Common.Domain;

namespace Evently.Modules.Events.Domain.Events;

public static class EventErrors
{
    public static Error NotFound(Guid eventId) =>
        Error.NotFound("Events.NotFound", $"The event with the identifier {eventId} was not found");

    public static readonly Error StartDateInPast = Error.Problem(
        "Events.StartDateInPast",
        "The event start date is in the past");

    public static readonly Error EndDatePrecedesStartDate = Error.Problem(
        "Events.EndDatePrecedesStartDate",
        "The event end date precedes the start date");

    public static readonly Error NoTicketsFound = Error.Problem(
        "Events.NoTicketsFound",
        "The event does not have any ticket types defined");

    public static readonly Error NotDraft = Error.Problem("Events.NotDraft", "The event is not in draft status");


    public static readonly Error AlreadyCanceled = Error.Problem(
        "Events.AlreadyCanceled",
        "The event was already canceled");


    public static readonly Error AlreadyStarted = Error.Problem(
        "Events.AlreadyStarted",
        "The event has already started");
}
