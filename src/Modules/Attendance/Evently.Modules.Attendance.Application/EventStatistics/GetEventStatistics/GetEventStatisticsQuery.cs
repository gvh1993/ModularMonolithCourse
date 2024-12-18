using Evently.Common.Application.Messaging;

namespace Evently.Modules.Attendance.Application.EventStatistics.GetEventStatistics;

public sealed record GetEventStatisticsQuery(Guid EventId) : IQuery<EventStatisticsResponse>;
