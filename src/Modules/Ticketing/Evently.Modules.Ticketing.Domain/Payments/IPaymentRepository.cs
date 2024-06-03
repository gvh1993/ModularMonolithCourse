using Evently.Modules.Ticketing.Domain.Events;

namespace Evently.Modules.Ticketing.Domain.Payments;

public interface IPaymentRepository
{
    Task<Payment?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Payment>> GetForEventAsync(Event @event, CancellationToken cancellationToken = default);

    void Insert(Payment payment);
}
