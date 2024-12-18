using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Customers.CreateCustomer;
using Evently.Modules.Ticketing.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Ticketing.IntegrationTests.Customers;

public class CreateCustomerTests : BaseIntegrationTest
{
    public CreateCustomerTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCommandIsInvalid()
    {
        //Arrange
        var command = new CreateCustomerCommand(
            Guid.NewGuid(),
            string.Empty,
            string.Empty,
            string.Empty);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.IsFailure.Should().BeTrue();
    }

    [Fact]
    public async Task Should_CreateCustomer_WhenCommandIsInvalid()
    {
        //Arrange
        var command = new CreateCustomerCommand(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}