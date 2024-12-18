using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Modules.Attendance.Domain.Attendees;

namespace Evently.Modules.Attendance.Application.EventStatistics.Projections;

internal sealed class AttendeeCheckedInDomainEventHandler(IDbConnectionFactory dbConnectionFactory)
    : DomainEventHandler<AttendeeCheckedInDomainEvent>
{
    public override async Task Handle(
        AttendeeCheckedInDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            """
            UPDATE attendance.event_statistics es
            SET attendees_checked_in = (
                SELECT COUNT(*)
                FROM attendance.tickets t
                WHERE
                    t.event_id = es.event_id AND
                    t.used_at_utc IS NOT NULL)
            WHERE es.event_id = @EventId
            """;

        await connection.ExecuteAsync(sql, domainEvent);
    }
}
