using Evently.Common.Application.EventBus;
using Evently.Common.Application.Messaging;
using Evently.Modules.Events.Domain.TicketTypes;
using Evently.Modules.Events.IntegrationEvents;

namespace Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;

internal sealed class TicketTypePriceChangedDomainEventHandler(IEventBus eventBus)
     : DomainEventHandler<TicketTypePriceChangedDomainEvent>
{
    public override async Task Handle(
        TicketTypePriceChangedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new TicketTypePriceChangedIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                domainEvent.TicketTypeId,
                domainEvent.Price),
            cancellationToken);
    }
}
