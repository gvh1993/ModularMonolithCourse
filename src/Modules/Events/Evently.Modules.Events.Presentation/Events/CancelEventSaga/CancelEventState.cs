using MassTransit;

namespace Evently.Modules.Events.Presentation.Events.CancelEventSaga;

public sealed class CancelEventState : SagaStateMachineInstance, ISagaVersion
{
    public Guid CorrelationId { get; set; }

    public int Version { get; set; }

    public string CurrentState { get; set; }

    public int CancellationCompletedStatus { get; set; }
}
