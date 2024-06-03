using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Orders;

public static class OrderErrors
{
    public static Error NotFound(Guid orderId) =>
        Error.NotFound("Orders.NotFound", $"The order with the identifier {orderId} was not found");


    public static readonly Error TicketsAlreadyIssues = Error.Problem(
        "Order.TicketsAlreadyIssued",
        "The tickets for this order were already issued");
}
