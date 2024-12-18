using Evently.Common.Domain;
using Evently.Modules.Events.Application.TicketTypes.CreateTicketType;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.IntegrationTests.TicketTypes;

public class CreateTicketTypeTests : BaseIntegrationTest
{
    public CreateTicketTypeTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventDoesNotExist()
    {
        // Arrange
        var command = new CreateTicketTypeCommand(
            Guid.NewGuid(),
            Faker.Commerce.ProductName(),
            Faker.Random.Decimal(),
            "USD",
            Faker.Random.Decimal());

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(EventErrors.NotFound(command.EventId));
    }

    [Fact]
    public async Task Should_CreateTicketType_WhenCommandIsValid()
    {
        // Arrange
        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());
        Guid eventId = await Sender.CreateEventAsync(categoryId);

        var command = new CreateTicketTypeCommand(
            eventId,
            Faker.Commerce.ProductName(),
            Faker.Random.Decimal(),
            "USD",
            Faker.Random.Decimal());

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }
}
