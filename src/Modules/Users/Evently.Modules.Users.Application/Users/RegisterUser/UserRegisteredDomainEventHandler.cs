using Evently.Common.Application.EventBus;
using Evently.Common.Application.Exceptions;
using Evently.Common.Application.Messaging;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUser;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.IntegrationEvents;
using MediatR;

namespace Evently.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus bus)
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(
        UserRegisteredDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<UserResponse> result = await sender.Send(
            new GetUserQuery(domainEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new EventlyException(nameof(GetUserQuery), result.Error);
        }

        await bus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
