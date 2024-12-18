using Evently.Common.Domain;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.Tickets;
using Evently.Modules.Ticketing.UnitTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Ticketing.UnitTests.Tickets;

public class TicketTests : BaseTest
{
    [Fact]
    public void Create_ShouldRaiseDomainEvent_WhenTicketIsCreated()
    {
        //Arrange
        var customer = Customer.Create(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        var order = Order.Create(customer);

        DateTime startsAtUtc = DateTime.UtcNow;
        var @event = Event.Create(
            Guid.NewGuid(),
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            startsAtUtc,
            null);

        var ticketType = TicketType.Create(
            Guid.NewGuid(),
            @event.Id,
            Faker.Name.FirstName(),
            Faker.Random.Decimal(),
            Faker.Random.String(3),
            Faker.Random.Decimal());

        //Act
        Result<Ticket> result = Ticket.Create(
            order,
            ticketType);

        //Assert
        TicketCreatedDomainEvent domainEvent =
            AssertDomainEventWasPublished<TicketCreatedDomainEvent>(result.Value);

        domainEvent.TicketId.Should().Be(result.Value.Id);
    }

    [Fact]
    public void Archive_ShouldRaiseDomainEvent_WhenTicketIsArchived()
    {
        //Arrange
        var customer = Customer.Create(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        var order = Order.Create(customer);

        DateTime startsAtUtc = DateTime.UtcNow;
        var @event = Event.Create(
            Guid.NewGuid(),
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            startsAtUtc,
            null);

        var ticketType = TicketType.Create(
            Guid.NewGuid(),
            @event.Id,
            Faker.Name.FirstName(),
            Faker.Random.Decimal(),
            Faker.Random.String(3),
            Faker.Random.Decimal());

        Result<Ticket> result = Ticket.Create(
            order,
            ticketType);

        //Act
        result.Value.Archive();

        //Assert
        TicketArchivedDomainEvent domainEvent =
            AssertDomainEventWasPublished<TicketArchivedDomainEvent>(result.Value);

        domainEvent.TicketId.Should().Be(result.Value.Id);
    }
}
