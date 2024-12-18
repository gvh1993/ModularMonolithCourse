using Evently.Modules.Attendance.Domain.Attendees;
using Evently.Modules.Attendance.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Attendance.Infrastructure.Attendees;

internal sealed class AttendeeRepository(AttendanceDbContext context) : IAttendeeRepository
{
    public async Task<Attendee?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Attendees.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(Attendee attendee)
    {
        context.Attendees.Add(attendee);
    }
}
