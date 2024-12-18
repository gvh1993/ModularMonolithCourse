using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Domain;
using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.Application.TicketTypes.UpdateTicketTypePrice;
using MediatR;

namespace Evently.Modules.Ticketing.Presentation.TicketTypes;

internal sealed class TicketTypePriceChangedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<TicketTypePriceChangedIntegrationEvent>
{
    public override async Task Handle(
        TicketTypePriceChangedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateTicketTypePriceCommand(integrationEvent.TicketTypeId, integrationEvent.Price),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(UpdateTicketTypePriceCommand), result.Error);
        }
    }
}
