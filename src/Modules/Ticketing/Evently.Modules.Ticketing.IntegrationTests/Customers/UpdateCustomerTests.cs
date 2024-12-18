using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Customers.UpdateCustomer;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Ticketing.IntegrationTests.Customers;

public class UpdateCustomerTests : BaseIntegrationTest
{
    public UpdateCustomerTests(IntegrationTestWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCustomerDoesNotExist()
    {
        //Arrange
        var command = new UpdateCustomerCommand(
            Guid.NewGuid(),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(CustomerErrors.NotFound(command.CustomerId));
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenCustomerIsUpdated()
    {
        //Arrange
        Guid customerId = await Sender.CreateCustomerAsync(Guid.NewGuid());

        var command = new UpdateCustomerCommand(
            customerId,
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}