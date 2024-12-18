using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Attendance.Application.Abstractions.Data;
using Evently.Modules.Attendance.Domain.Attendees;

namespace Evently.Modules.Attendance.Application.Attendees.CreateAttendee;

internal sealed class CreateAttendeeCommandHandler(IAttendeeRepository attendeeRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAttendeeCommand>
{
    public async Task<Result> Handle(CreateAttendeeCommand request, CancellationToken cancellationToken)
    {
        var attendee = Attendee.Create(request.AttendeeId, request.Email, request.FirstName, request.LastName);

        attendeeRepository.Insert(attendee);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
