using Evently.Common.Application.Messaging;
using Evently.Modules.Ticketing.Application.Abstractions.Payments;
using Evently.Modules.Ticketing.Domain.Payments;

namespace Evently.Modules.Ticketing.Application.Payments.RefundPayment;

internal sealed class PaymentRefundedDomainEventHandler(IPaymentService paymentService)
    : DomainEventHandler<PaymentRefundedDomainEvent>
{
    public override async Task Handle(
        PaymentRefundedDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        await paymentService.RefundAsync(domainEvent.TransactionId, domainEvent.RefundAmount);
    }
}
