using Evently.Common.Domain;
using Evently.Modules.Events.Application.Categories.CreateCategory;
using Evently.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.IntegrationTests.Categories;

public class CreateCategoryTests : BaseIntegrationTest
{
    public CreateCategoryTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_CreateCategory_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateCategoryCommand("Category name");

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCommandIsNotValid()
    {
        // Arrange
        var command = new CreateCategoryCommand("");

        // Act
        Result<Guid> result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Type.Should().Be(ErrorType.Validation);
    }
}
