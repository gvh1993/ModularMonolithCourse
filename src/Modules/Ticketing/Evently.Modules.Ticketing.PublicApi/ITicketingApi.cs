namespace Evently.Modules.Ticketing.PublicApi;

public interface ITicketingApi
{
    Task CreateCustomerAsync(
        Guid customerId,
        string email,
        string firstName,
        string lastName,
        CancellationToken cancellationToken = default);
}
