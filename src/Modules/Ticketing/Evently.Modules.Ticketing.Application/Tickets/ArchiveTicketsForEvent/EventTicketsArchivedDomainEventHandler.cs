using Evently.Common.Application.EventBus;
using Evently.Common.Application.Messaging;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.IntegrationEvents;

namespace Evently.Modules.Ticketing.Application.Tickets.ArchiveTicketsForEvent;

internal sealed class EventTicketsArchivedDomainEventHandler(IEventBus eventBus)
     : DomainEventHandler<EventTicketsArchivedDomainEvent>
{
    public override async Task Handle(
        EventTicketsArchivedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await eventBus.PublishAsync(
            new EventTicketsArchivedIntegrationEvent(
                domainEvent.EventId,
                domainEvent.OccurredOnUtc,
                domainEvent.EventId),
            cancellationToken);
    }
}
