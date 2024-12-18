using Evently.Common.Domain;
using Evently.Modules.Events.Application.Categories.GetCategory;
using Evently.Modules.Events.Domain.Categories;
using Evently.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Events.IntegrationTests.Categories;

public class GetCategoryTests : BaseIntegrationTest
{
    public GetCategoryTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCategoryDoesNotExist()
    {
        // Arrange
        var query = new GetCategoryQuery(Guid.NewGuid());

        // Act
        Result result = await Sender.Send(query);

        // Assert
        result.Error.Should().Be(CategoryErrors.NotFound(query.CategoryId));
    }

    [Fact]
    public async Task Should_ReturnCategory_WhenCategoryExists()
    {
        // Arrange
        Guid categoryId = await Sender.CreateCategoryAsync(Faker.Music.Genre());

        var query = new GetCategoryQuery(categoryId);

        // Act
        Result<CategoryResponse> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}
