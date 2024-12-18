using Evently.Common.Application.Messaging;

namespace Evently.Modules.Attendance.Application.Attendees.CreateAttendee;

public sealed record CreateAttendeeCommand(Guid AttendeeId, string Email, string FirstName, string LastName)
    : ICommand;
