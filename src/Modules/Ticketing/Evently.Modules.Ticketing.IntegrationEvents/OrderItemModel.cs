namespace Evently.Modules.Ticketing.IntegrationEvents;

public sealed class OrderItemModel
{
    public Guid Id { get; init; }

    public Guid OrderId { get; init; }

    public Guid TicketTypeId { get; init; }

    public decimal Quantity { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal Price { get; init; }

    public string Currency { get; init; }
}
