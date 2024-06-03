using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Events.RescheduleEvent;

public sealed record RescheduleEventCommand(Guid EventId, DateTime StartsAtUtc, DateTime? EndsAtUtc) : ICommand;
