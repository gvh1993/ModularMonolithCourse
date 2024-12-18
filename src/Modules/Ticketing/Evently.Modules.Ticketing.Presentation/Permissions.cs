namespace Evently.Modules.Ticketing.Presentation;

internal static class Permissions
{
    internal const string GetCart = "carts:read";
    internal const string AddToCart = "carts:add";
    internal const string RemoveFromCart = "carts:remove";
    internal const string GetOrders = "orders:read";
    internal const string CreateOrder = "orders:create";
    internal const string GetTickets = "tickets:read";
}
