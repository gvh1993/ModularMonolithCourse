using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.IntegrationTests.Events;

public class CreateEventTests : BaseIntegrationTest
{
    public CreateEventTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenStartDateInPast()
    {
        // Arrange
        var command = new CreateEventCommand(
            Guid.NewGuid(),
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            DateTime.UtcNow.AddMinutes(-1),
            null);

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(EventErrors.StartDateInPast);
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        var command = new CreateEventCommand(
            categoryId,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            DateTime.UtcNow.AddMinutes(10),
            null);

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(CategoryErrors.NotFound(categoryId));
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEndDatePrecedesStartDate()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        DateTime startsAtUtc = DateTime.UtcNow.AddMinutes(10);
        DateTime endsAtUtc = startsAtUtc.AddMinutes(-5);

        var command = new CreateEventCommand(
            categoryId,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            startsAtUtc,
            endsAtUtc);

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.Error.Type.Should().Be(ErrorType.Validation);
    }

    [Fact]
    public async Task Should_CreateEvent_WhenCommandIsValid()
    {
        // Arrange
        await CleanDatabaseAsync();
        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());

        var command = new CreateEventCommand(
            categoryId,
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetAddress(),
            DateTime.UtcNow.AddMinutes(10),
            null);

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }
}
