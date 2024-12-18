using FluentValidation;

namespace Evently.Modules.Attendance.Application.Attendees.CreateAttendee;

internal sealed class CreateAttendeeCommandValidator : AbstractValidator<CreateAttendeeCommand>
{
    public CreateAttendeeCommandValidator()
    {
        RuleFor(c => c.AttendeeId).NotEmpty();
        RuleFor(c => c.Email).EmailAddress();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
    }
}
