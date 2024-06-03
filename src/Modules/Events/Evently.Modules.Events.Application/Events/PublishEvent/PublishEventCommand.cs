using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.PublishEvent;

public sealed record PublishEventCommand(Guid EventId) : ICommand;
