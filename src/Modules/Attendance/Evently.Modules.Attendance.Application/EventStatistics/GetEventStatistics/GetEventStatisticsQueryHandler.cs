using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Attendance.Domain.Events;

namespace Evently.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

internal sealed class GetEventStatisticsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetEventStatisticsQuery, EventStatisticsResponse>
{
    public async Task<Result<EventStatisticsResponse>> Handle(
        GetEventStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 event_id AS {nameof(EventStatisticsResponse.EventId)},
                 title AS {nameof(EventStatisticsResponse.Title)},
                 description AS {nameof(EventStatisticsResponse.Description)},
                 location AS {nameof(EventStatisticsResponse.Location)},
                 starts_at_utc AS {nameof(EventStatisticsResponse.StartsAtUtc)},
                 ends_at_utc AS {nameof(EventStatisticsResponse.EndsAtUtc)},
                 tickets_sold AS {nameof(EventStatisticsResponse.TicketsSold)},
                 attendees_checked_in AS {nameof(EventStatisticsResponse.AttendeesCheckedIn)},
                 duplicate_check_in_tickets AS {nameof(EventStatisticsResponse.DuplicateCheckInTickets)},
                 invalid_check_in_tickets AS {nameof(EventStatisticsResponse.InvalidCheckInTickets)}
             FROM attendance.event_statistics
             WHERE event_id = @EventId
             """;

        EventStatisticsResponse? eventStatistics =
            await connection.QuerySingleOrDefaultAsync<EventStatisticsResponse>(sql, request);

        if (eventStatistics is null)
        {
            return Result.Failure<EventStatisticsResponse>(EventErrors.NotFound(request.EventId));
        }

        return eventStatistics;
    }
}
