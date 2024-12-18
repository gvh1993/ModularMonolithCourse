namespace Evently.Modules.Attendance.Application.Abstractions.Authentication;

public interface IAttendanceContext
{
    Guid AttendeeId { get; }
}
