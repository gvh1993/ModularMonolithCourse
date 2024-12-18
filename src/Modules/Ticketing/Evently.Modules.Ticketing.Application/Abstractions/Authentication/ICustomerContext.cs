namespace Evently.Modules.Ticketing.Application.Abstractions.Authentication;

public interface ICustomerContext
{
    Guid CustomerId { get; }
}
