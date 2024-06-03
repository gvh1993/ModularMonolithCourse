namespace Evently.Modules.Ticketing.Application.Tickets.GetTicket;

public sealed record TicketResponse(
    Guid Id,
    Guid CustomerId,
    Guid OrderId,
    Guid EventId,
    Guid TicketTypeId,
    string Code,
    DateTime CreatedAtUtc);
