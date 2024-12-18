using Evently.Common.Application.Authorization;
using Evently.Common.Domain;
using Evently.Modules.Users.Application.Users.GetUserPermissions;
using Evently.Modules.Users.Application.Users.RegisterUser;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Users.IntegrationTests.Users;

public class GetUserPermissionTests : BaseIntegrationTest
{
    public GetUserPermissionTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        string identityId = Guid.NewGuid().ToString();

        // Act
        Result<PermissionsResponse> permissionsResult = await Sender.Send(new GetUserPermissionsQuery(identityId));

        // Assert
        permissionsResult.Error.Should().Be(UserErrors.NotFound(identityId));
    }

    [Fact]
    public async Task Should_ReturnPermissions_WhenUserExists()
    {
        // Arrange
        Result<Guid> result = await Sender.Send(new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Internet.Password(),
            Faker.Name.FirstName(),
            Faker.Name.LastName()));

        string identityId = DbContext.Users.Single(u => u.Id == result.Value).IdentityId;

        // Act
        Result<PermissionsResponse> permissionsResult = await Sender.Send(new GetUserPermissionsQuery(identityId));

        // Assert
        permissionsResult.IsSuccess.Should().BeTrue();
        permissionsResult.Value.Permissions.Should().NotBeEmpty();
    }
}
