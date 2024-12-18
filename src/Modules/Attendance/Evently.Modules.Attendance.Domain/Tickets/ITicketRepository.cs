namespace Evently.Modules.Attendance.Domain.Tickets;

public interface ITicketRepository
{
    Task<Ticket?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Ticket ticket);
}
