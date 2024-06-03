using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.Payments;
using Evently.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Ticketing.Infrastructure.Payments;

internal sealed class PaymentRepository(TicketingDbContext context) : IPaymentRepository
{
    public async Task<Payment?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Payments.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Payment>> GetForEventAsync(
        Event @event,
        CancellationToken cancellationToken = default)
    {
        return await (
            from order in context.Orders
            join payment in context.Payments on order.Id equals payment.OrderId
            join orderItem in context.OrderItems on order.Id equals orderItem.OrderId
            join ticketType in context.TicketTypes on orderItem.TicketTypeId equals ticketType.Id
            where ticketType.EventId == @event.Id
            select payment).ToListAsync(cancellationToken);
    }

    public void Insert(Payment payment)
    {
        context.Payments.Add(payment);
    }
}
