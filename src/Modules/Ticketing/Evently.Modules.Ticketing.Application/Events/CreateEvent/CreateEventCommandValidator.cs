using FluentValidation;

namespace Evently.Modules.Ticketing.Application.Events.CreateEvent;

internal sealed class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(c => c.EventId).NotEmpty();
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.Location).NotEmpty();
        RuleFor(c => c.StartsAtUtc).NotEmpty();
        RuleFor(c => c.EndsAtUtc).Must((cmd, endsAt) => endsAt > cmd.StartsAtUtc).When(c => c.EndsAtUtc.HasValue);

        RuleForEach(c => c.TicketTypes)
            .ChildRules(t =>
            {
                t.RuleFor(r => r.EventId).NotEmpty();
                t.RuleFor(r => r.Name).NotEmpty();
                t.RuleFor(r => r.Price).GreaterThan(decimal.Zero);
                t.RuleFor(r => r.Currency).NotEmpty();
                t.RuleFor(r => r.Quantity).GreaterThan(decimal.Zero);
            });
    }
}
