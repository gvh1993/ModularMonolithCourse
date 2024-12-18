using Evently.Modules.Events.IntegrationEvents;
using Evently.Modules.Ticketing.IntegrationEvents;
using MassTransit;

namespace Evently.Modules.Events.Presentation.Events.CancelEventSaga;

public sealed class CancelEventSaga : MassTransitStateMachine<CancelEventState>
{
    public State CancellationStarted { get; private set; }
    public State PaymentsRefunded { get; private set; }
    public State TicketsArchived { get; private set; }

    public Event<EventCanceledIntegrationEvent> EventCanceled { get; private set; }
    public Event<EventPaymentsRefundedIntegrationEvent> EventPaymentsRefunded { get; private set; }
    public Event<EventTicketsArchivedIntegrationEvent> EventTicketsArchived { get; private set; }
    public Event EventCancellationCompleted { get; private set; }

    public CancelEventSaga()
    {
        Event(() => EventCanceled, c => c.CorrelateById(m => m.Message.EventId));
        Event(() => EventPaymentsRefunded, c => c.CorrelateById(m => m.Message.EventId));
        Event(() => EventTicketsArchived, c => c.CorrelateById(m => m.Message.EventId));

        InstanceState(s => s.CurrentState);

        Initially(
            When(EventCanceled)
                .Publish(context =>
                    new EventCancellationStartedIntegrationEvent(
                            context.Message.Id,
                            context.Message.OccurredOnUtc,
                            context.Message.EventId))
                .TransitionTo(CancellationStarted));

        During(CancellationStarted,
            When(EventPaymentsRefunded)
                .TransitionTo(PaymentsRefunded),
            When(EventTicketsArchived)
                .TransitionTo(TicketsArchived));

        During(PaymentsRefunded,
            When(EventTicketsArchived)
                .TransitionTo(TicketsArchived));

        During(TicketsArchived,
            When(EventPaymentsRefunded)
                .TransitionTo(PaymentsRefunded));

        CompositeEvent(
            () => EventCancellationCompleted,
            state => state.CancellationCompletedStatus,
            EventPaymentsRefunded, EventTicketsArchived);

        DuringAny(
            When(EventCancellationCompleted)
                .Publish(context =>
                    new EventCancellationCompletedIntegrationEvent(
                        Guid.NewGuid(),
                        DateTime.UtcNow,
                        context.Saga.CorrelationId))
                .Finalize());
    }
}
