using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Customers.GetCustomer;
using Evently.Modules.Ticketing.Domain.Customers;
using Evently.Modules.Ticketing.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Ticketing.IntegrationTests.Customers;

public class GetCustomerByIdTests : BaseIntegrationTest
{
    public GetCustomerByIdTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenCustomerDoesNotExist()
    {
        // Arrange
        var query = new GetCustomerQuery(Guid.NewGuid());

        // Act
        Result result = await Sender.Send(query);

        // Assert
        result.Error.Should().Be(CustomerErrors.NotFound(query.CustomerId));
    }

    [Fact]
    public async Task Should_ReturnCustomer_WhenCustomerExists()
    {
        // Arrange
        Guid customerId = await Sender.CreateCustomerAsync(Guid.NewGuid());

        var query = new GetCustomerQuery(customerId);

        // Act
        Result<CustomerResponse> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
    }
}