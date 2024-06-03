using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Carts.ClearCart;

public sealed record ClearCartCommand(Guid CustomerId) : ICommand;
