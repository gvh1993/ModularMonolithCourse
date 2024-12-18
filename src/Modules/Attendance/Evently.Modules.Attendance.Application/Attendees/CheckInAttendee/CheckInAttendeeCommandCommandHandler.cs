using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Attendance.Application.Abstractions.Data;
using Evently.Modules.Attendance.Domain.Attendees;
using Evently.Modules.Attendance.Domain.Tickets;
using Microsoft.Extensions.Logging;

namespace Evently.Modules.Attendance.Application.Attendees.CheckInAttendee;

internal sealed class CheckInAttendeeCommandCommandHandler(
    IAttendeeRepository attendeeRepository,
    ITicketRepository ticketRepository,
    IUnitOfWork unitOfWork,
    ILogger<CheckInAttendeeCommandCommandHandler> logger)
    : ICommandHandler<CheckInAttendeeCommand>
{
    public async Task<Result> Handle(CheckInAttendeeCommand request, CancellationToken cancellationToken)
    {
        Attendee? attendee = await attendeeRepository.GetAsync(request.AttendeeId, cancellationToken);

        if (attendee is null)
        {
            return Result.Failure(AttendeeErrors.NotFound(request.AttendeeId));
        }

        Ticket? ticket = await ticketRepository.GetAsync(request.TicketId, cancellationToken);

        if (ticket is null)
        {
            return Result.Failure(TicketErrors.NotFound);
        }

        Result result = attendee.CheckIn(ticket);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        if (result.IsFailure)
        {
            logger.LogWarning(
                "Check in failed: {AttendeeId}, {TicketId}, {@Error}",
                attendee.Id,
                ticket.Id,
                result.Error);
        }

        return result;
    }
}
