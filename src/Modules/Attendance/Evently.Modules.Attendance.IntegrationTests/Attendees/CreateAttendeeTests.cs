using Evently.Common.Domain;
using Evently.Modules.Attendance.Application.Attendees.CreateAttendee;
using Evently.Modules.Attendance.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Attendance.IntegrationTests.Attendees;

public class CreateAttendeeTests : BaseIntegrationTest
{
    public CreateAttendeeTests(IntegrationTestWebAppFactory factory)
       : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCommandIsInvalid()
    {
        // Arrange
        var command = new CreateAttendeeCommand(
            Guid.NewGuid(),
            string.Empty,
            string.Empty,
            string.Empty);

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenCommandIsValid()
    {
        // Arrange
        var command = new CreateAttendeeCommand(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        // Act
        Result result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
