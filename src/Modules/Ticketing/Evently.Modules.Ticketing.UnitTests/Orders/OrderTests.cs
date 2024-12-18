using Evently.Common.Domain;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.UnitTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Ticketing.UnitTests.Orders;

public class OrderTests : BaseTest
{
    [Fact]
    public void Create_ShouldRaiseDomainEvent_WhenOrderIsCreated()
    {
        //Arrange
        var customer = Customer.Create(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        //Act
        Result<Order> result = Order.Create(customer);

        //Assert
        OrderCreatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<OrderCreatedDomainEvent>(result.Value);

        domainEvent.OrderId.Should().Be(result.Value.Id);
    }

    [Fact]
    public void IssueTicket_ShouldReturnFailure_WhenTicketAlreadyIssued()
    {
        //Arrange
        var customer = Customer.Create(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        Result<Order> result = Order.Create(customer);

        result.Value.IssueTickets();
        Order order = result.Value;

        //Act
        Result issueTicketsResult = order.IssueTickets();

        //Assert
        issueTicketsResult.Error.Should().Be(OrderErrors.TicketsAlreadyIssues);
    }

    [Fact]
    public void IssueTicket_ShouldRaiseDomainEvent_WhenTicketIsIssued()
    {
        //Arrange
        var customer = Customer.Create(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        Result<Order> result = Order.Create(customer);

        //Act
        result.Value.IssueTickets();

        //Assert
        OrderTicketsIssuedDomainEvent domainEvent =
            AssertDomainEventWasPublished<OrderTicketsIssuedDomainEvent>(result.Value);

        domainEvent.OrderId.Should().Be(result.Value.Id);
    }
}
