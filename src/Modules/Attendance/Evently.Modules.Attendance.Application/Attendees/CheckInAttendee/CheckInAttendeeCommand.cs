using Evently.Common.Application.Messaging;

namespace Evently.Modules.Attendance.Application.Attendees.CheckInAttendee;

public sealed record CheckInAttendeeCommand(Guid AttendeeId, Guid TicketId) : ICommand;
