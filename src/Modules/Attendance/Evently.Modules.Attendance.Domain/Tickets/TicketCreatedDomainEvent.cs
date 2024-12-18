using Evently.Common.Domain;

namespace Evently.Modules.Attendance.Domain.Tickets;

public sealed class TicketCreatedDomainEvent(Guid ticketId, Guid eventId) : DomainEvent
{
    public Guid TicketId { get; init; } = ticketId;

    public Guid EventId { get; init; } = eventId;
}
