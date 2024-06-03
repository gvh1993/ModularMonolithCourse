using Evently.Common.Domain;

namespace Evently.Modules.Ticketing.Domain.Payments;

public sealed class PaymentRefundedDomainEvent(Guid paymentId, Guid transactionId, decimal refundAmount)
    : DomainEvent
{
    public Guid PaymentId { get; init; } = paymentId;

    public Guid TransactionId { get; init; } = transactionId;

    public decimal RefundAmount { get; init; } = refundAmount;
}
