using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Ticketing.Infrastructure.Events;

internal sealed class TicketTypeRepository(TicketingDbContext context) : ITicketTypeRepository
{
    public async Task<TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.TicketTypes.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<TicketType?> GetWithLockAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context
            .TicketTypes
            .FromSql(
                $"""
                SELECT id, event_id, name, price, currency, quantity, available_quantity
                FROM ticketing.ticket_types
                WHERE id = {id}
                FOR UPDATE NOWAIT
                """)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public void InsertRange(IEnumerable<TicketType> ticketTypes)
    {
        context.TicketTypes.AddRange(ticketTypes);
    }
}
