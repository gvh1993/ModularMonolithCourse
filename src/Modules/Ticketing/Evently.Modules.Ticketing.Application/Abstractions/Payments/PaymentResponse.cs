namespace Evently.Modules.Ticketing.Application.Abstractions.Payments;

public sealed record PaymentResponse(Guid TransactionId, decimal Amount, string Currency);
