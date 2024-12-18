using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Attendees;

public sealed class InvalidCheckInAttemptedDomainEvent(Guid attendeeId, Guid eventId, Guid ticketId, string ticketCode)
    : DomainEvent
{
    public Guid AttendeeId { get; init; } = attendeeId;

    public Guid EventId { get; init; } = eventId;

    public Guid TicketId { get; init; } = ticketId;

    public string TicketCode { get; init; } = ticketCode;
}
