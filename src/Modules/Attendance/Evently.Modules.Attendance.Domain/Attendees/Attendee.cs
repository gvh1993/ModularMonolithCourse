using Evently.Common.Domain;
using Evently.Modules.Attendance.Domain.Tickets;

namespace Evently.Modules.Attendance.Domain.Attendees;

public sealed class Attendee : Entity
{
    private Attendee()
    {
    }

    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public static Attendee Create(Guid id, string email, string firstName, string lastName)
    {
        return new Attendee
        {
            Id = id,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };
    }

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public Result CheckIn(Ticket ticket)
    {
        if (Id != ticket.AttendeeId)
        {
            Raise(new InvalidCheckInAttemptedDomainEvent(Id, ticket.EventId, ticket.Id, ticket.Code));

            return Result.Failure(TicketErrors.InvalidCheckIn);
        }

        if (ticket.UsedAtUtc.HasValue)
        {
            Raise(new DuplicateCheckInAttemptedDomainEvent(Id, ticket.EventId, ticket.Id, ticket.Code));

            return Result.Failure(TicketErrors.DuplicateCheckIn);
        }

        ticket.MarkAsUsed();

        Raise(new AttendeeCheckedInDomainEvent(Id, ticket.EventId));

        return Result.Success();
    }
}
