using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Payments.RefundPaymentsForEvent;
using Evently.Modules.Ticketing.Domain.Events;
using MediatR;

namespace Evently.Modules.Ticketing.Application.Events.CancelEvent;

internal sealed class RefundPaymentsEventCanceledDomainEventHandler(ISender sender)
    : DomainEventHandler<EventCanceledDomainEvent>
{
    public override async Task Handle(
        EventCanceledDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new RefundPaymentsForEventCommand(domainEvent.EventId), cancellationToken);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(RefundPaymentsForEventCommand), result.Error);
        }
    }
}
