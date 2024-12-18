using Evently.Common.Application.Messaging;

namespace Evently.Modules.Attendance.Application.Attendees.GetAttendee;

public sealed record GetAttendeeQuery(Guid CustomerId) : IQuery<AttendeeResponse>;
