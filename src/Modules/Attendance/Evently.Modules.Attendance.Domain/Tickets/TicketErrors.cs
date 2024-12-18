using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Tickets;

public static class TicketErrors
{
    public static readonly Error NotFound = Error.Problem("Tickets.NotFound", "The ticket was not found");

    public static readonly Error InvalidCheckIn = Error.Problem(
        "Tickets.InvalidCheckIn",
        "The ticket check in was invalid");

    public static readonly Error DuplicateCheckIn = Error.Problem(
        "Tickets.DuplicateCheckIn",
        "The ticket was already checked in");
}
