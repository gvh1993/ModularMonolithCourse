using FluentValidation;

namespace Evently.Modules.Attendance.Application.Tickets.CreateTicket;

internal sealed class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
{
    public CreateTicketCommandValidator()
    {
        RuleFor(c => c.TicketId).NotEmpty();
        RuleFor(c => c.AttendeeId).NotEmpty();
        RuleFor(c => c.EventId).NotEmpty();
        RuleFor(c => c.Code).NotEmpty();
    }
}
