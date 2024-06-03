namespace Evently.Modules.Events.PublicApi;

public sealed record TicketTypeResponse(
    Guid Id,
    Guid EventId,
    string Name,
    decimal Price,
    string Currency,
    decimal Quantity);
