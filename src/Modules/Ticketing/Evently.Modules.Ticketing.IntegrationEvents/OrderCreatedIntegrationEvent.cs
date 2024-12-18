using Evently.Common.Application.EventBus;

namespace Evently.Modules.Ticketing.IntegrationEvents;

public sealed class OrderCreatedIntegrationEvent : IntegrationEvent
{
    public OrderCreatedIntegrationEvent(
        Guid id,
        DateTime occuredOnUtc,
        Guid orderId,
        Guid customerId,
        decimal totalPrice,
        DateTime createdAtUtc,
        List<OrderItemModel> orderItems)
        : base(id, occuredOnUtc)
    {
        OrderId = orderId;
        CustomerId = customerId;
        TotalPrice = totalPrice;
        CreatedAtUtc = createdAtUtc;
        OrderItems = orderItems;
    }

    public Guid OrderId { get; init; }

    public Guid CustomerId { get; init; }

    public decimal TotalPrice { get; init; }

    public DateTime CreatedAtUtc { get; init; }

    public List<OrderItemModel> OrderItems { get; init; }
}
