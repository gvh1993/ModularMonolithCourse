using Evently.Common.Domain;
using Evently.Modules.Events.Application.Events.GetEvents;
using Evently.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.IntegrationTests.Events;

public class GetEventsTests : BaseIntegrationTest
{
    public GetEventsTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventsDoNotExist()
    {
        // Arrange
        await CleanDatabaseAsync();

        var query = new GetEventsQuery();

        // Act
        Result<IReadOnlyCollection<EventResponse>> result = await Sender.Send(query);

        // Assert
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_ReturnEvents_WhenEventsExist()
    {
        // Arrange
        await CleanDatabaseAsync();

        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());

        await Sender.CreateEventAsync(categoryId);
        await Sender.CreateEventAsync(categoryId);

        var query = new GetEventsQuery();

        // Act
        Result<IReadOnlyCollection<EventResponse>> result = await Sender.Send(query);

        // Assert
        result.Value.Should().HaveCount(2);
    }
}
