using Evently.Common.Application.Messaging;

namespace Evently.Modules.Events.Application.Events.GetEvent;

public sealed record GetEventQuery(Guid EventId) : IQuery<EventResponse>;
