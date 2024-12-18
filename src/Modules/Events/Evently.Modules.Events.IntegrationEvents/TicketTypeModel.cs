namespace Evently.Modules.Events.IntegrationEvents;

public sealed class TicketTypeModel
{
    public Guid Id { get; init; }

    public Guid EventId { get; init; }

    public string Name { get; init; }

    public decimal Price { get; init; }

    public string Currency { get; init; }

    public decimal Quantity { get; init; }
}
