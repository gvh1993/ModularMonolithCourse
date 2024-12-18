using Evently.Common.Domain;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.Payments;
using Evently.Modules.Ticketing.UnitTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Ticketing.UnitTests.Payments;

public class PaymentTests : BaseTest
{
    [Fact]
    public void Create_ShouldRaiseDomainEvent_WhenPaymentIsCreated()
    {
        //Arrange
        var customer = Customer.Create(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        var order = Order.Create(customer);

        //Act
        Result<Payment> result = Payment.Create(
            order,
            Guid.NewGuid(),
            Faker.Random.Decimal(),
            Faker.Random.String(3));

        //Assert
        PaymentCreatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<PaymentCreatedDomainEvent>(result.Value);

        domainEvent.PaymentId.Should().Be(result.Value.Id);
    }

    [Fact]
    public void Refund_ShouldReturnFailure_WhenAlreadyRefunded()
    {
        //Arrange
        decimal amount = Faker.Random.Decimal();

        var customer = Customer.Create(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        var order = Order.Create(customer);

        Result<Payment> paymentResult = Payment.Create(
            order,
            Guid.NewGuid(),
            amount,
            Faker.Random.String(3));

        Payment payment = paymentResult.Value;

        payment.Refund(amount);

        //Act
        Result result = payment.Refund(amount);

        //Assert
        result.Error.Should().Be(PaymentErrors.AlreadyRefunded);
    }
}
