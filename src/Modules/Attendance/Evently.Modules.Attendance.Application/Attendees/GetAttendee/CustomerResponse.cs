namespace Evently.Modules.Attendance.Application.Attendees.GetAttendee;

public sealed record AttendeeResponse(Guid Id, string Email, string FirstName, string LastName);
