using FluentValidation;

namespace Evently.Modules.Attendance.Application.Attendees.UpdateAttendee;

internal sealed class UpdateAttendeeCommandValidator : AbstractValidator<UpdateAttendeeCommand>
{
    public UpdateAttendeeCommandValidator()
    {
        RuleFor(c => c.AttendeeId).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}
