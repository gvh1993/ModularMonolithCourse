using Evently.Common.Domain;
using Evently.Modules.Events.Application.Categories.ArchiveCategory;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.IntegrationTests.Categories;

public class ArchiveCategoryTests : BaseIntegrationTest
{
    public ArchiveCategoryTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCategoryDoesNotExist()
    {
        // Arrange
        var command = new ArchiveCategoryCommand(Guid.NewGuid());

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(CategoryErrors.NotFound(command.CategoryId));
    }

    [Fact]
    public async Task Should_ArchiveCategory_WhenCategoryExists()
    {
        // Arrange
        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());

        var command = new ArchiveCategoryCommand(categoryId);

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCategoryAlreadyArchived()
    {
        // Arrange
        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());

        var command = new ArchiveCategoryCommand(categoryId);

        await Sender.Send(command);

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.Error.Should().Be(CategoryErrors.AlreadyArchived);
    }
}
