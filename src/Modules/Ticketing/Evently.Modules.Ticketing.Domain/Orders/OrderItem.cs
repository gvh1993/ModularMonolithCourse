namespace Evently.Modules.Ticketing.Domain.Orders;

public sealed class OrderItem
{
    private OrderItem()
    {
    }

    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid TicketTypeId { get; private set; }

    public decimal Quantity { get; private set; }

    public decimal UnitPrice { get; private set; }

    public decimal Price { get; private set; }

    public string Currency { get; private set; }

    internal static OrderItem Create(Guid orderId, Guid ticketTypeId, decimal quantity, decimal unitPrice, string currency)
    {
        var orderItem = new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            TicketTypeId = ticketTypeId,
            Quantity = quantity,
            UnitPrice = unitPrice,
            Price = quantity * unitPrice,
            Currency = currency
        };

        return orderItem;
    }

}
