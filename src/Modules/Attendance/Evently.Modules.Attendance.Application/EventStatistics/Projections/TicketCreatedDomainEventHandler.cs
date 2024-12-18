using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Tickets;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

internal sealed class TicketCreatedDomainEventHandler(IDbConnectionFactory dbConnectionFactory)
    : DomainEventHandler<TicketCreatedDomainEvent>
{
    public override async Task Handle(
        TicketCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            """
            UPDATE attendance.event_statistics es
            SET tickets_sold = (
                SELECT COUNT(*)
                FROM attendance.tickets t
                WHERE t.event_id = es.event_id)
            WHERE es.event_id = @EventId
            """;

        await connection.ExecuteAsync(sql, domainEvent);
    }
}
