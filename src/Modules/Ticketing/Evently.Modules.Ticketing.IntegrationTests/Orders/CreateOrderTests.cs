using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Carts;
using Evently.Modules.Ticketing.Application.Orders.CreateOrder;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Ticketing.IntegrationTests.Orders;

public class CreateOrderTests : BaseIntegrationTest
{
    public CreateOrderTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCustomerDoesNotExist()
    {
        //Arrange
        var command = new CreateOrderCommand(Guid.NewGuid());

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(CustomerErrors.NotFound(command.CustomerId));
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCartIsEmpty()
    {
        //Arrange
        Guid customerId = await Sender.CreateCustomerAsync(Guid.NewGuid());

        var command = new CreateOrderCommand(customerId);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(CartErrors.Empty);
    }
}
