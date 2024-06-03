using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.CancelEvent;

public sealed record CancelEventCommand(Guid EventId) : ICommand;
