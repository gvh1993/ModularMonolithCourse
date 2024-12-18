using Evently.Common.Domain;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.UnitTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Ticketing.UnitTests.Events
{
    public class TicketTypeTests : BaseTest
    {
        [Fact]
        public void Create_ShouldReturnValue_WhenTicketTypeIsCreated()
        {
            //Arrange
            DateTime startsAtUtc = DateTime.UtcNow;
            var @event = Event.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Address.StreetAddress(),
                startsAtUtc,
                null);

            //Act
            Result<TicketType> result = TicketType.Create(
                Guid.NewGuid(),
                @event.Id,
                Faker.Name.FirstName(),
                Faker.Random.Decimal(),
                Faker.Random.String(3),
                Faker.Random.Decimal());

            //Assert
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public void UpdateQuantity_ShouldReturnFailure_WhenNotEnoughQuanitity()
        {
            //Arrange
            DateTime startsAtUtc = DateTime.UtcNow;
            var @event = Event.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Address.StreetAddress(),
                startsAtUtc,
                null);

            decimal quantity = Faker.Random.Decimal();
            var ticketType = TicketType.Create(
            Guid.NewGuid(),
            @event.Id,
            Faker.Name.FirstName(),
            Faker.Random.Decimal(),
            Faker.Random.String(3),
            quantity);

            //Act
            Result result = ticketType.UpdateQuantity(quantity + 1);

            //Assert
            result.Error.Should().Be(TicketTypeErrors.NotEnoughQuantity(quantity));
        }

        [Fact]
        public void UpdateQuantity_ShouldRaiseDomainEvent_WhenTicketTypesIsSoldOut()
        {
            //Arrange
            DateTime startsAtUtc = DateTime.UtcNow;
            var @event = Event.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Address.StreetAddress(),
                startsAtUtc,
                null);

            decimal quantity = Faker.Random.Decimal();
            Result<TicketType> ticketType = TicketType.Create(
            Guid.NewGuid(),
            @event.Id,
            Faker.Name.FirstName(),
            Faker.Random.Decimal(),
            Faker.Random.String(3),
            quantity);

            //Act
            ticketType.Value.UpdateQuantity(quantity);

            //Assert
            TicketTypeSoldOutDomainEvent domainEvent = AssertDomainEventWasPublished<TicketTypeSoldOutDomainEvent>(ticketType.Value);

            domainEvent.TicketTypeId.Should().Be(ticketType.Value.Id);
        }
    }
}
