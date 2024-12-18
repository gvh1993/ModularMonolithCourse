using Evently.Common.Domain;
using Evently.IntegrationTests.Abstractions;
using Evently.Modules.Attendance.Application.Attendees.GetAttendee;
using Evently.Modules.Ticketing.Application.Customers.GetCustomer;
using Evently.Modules.Users.Application.Users.RegisterUser;
using FluentAssertions;

namespace Evently.IntegrationTests.RegisterUser;

public sealed class RegisterUserTests : BaseIntegrationTest
{
    public RegisterUserTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task RegisterUser_Should_PropagateToTicketingModule()
    {
        // Register user
        var command = new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Internet.Password(6),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        Result<Guid> userResult = await Sender.Send(command);

        userResult.IsSuccess.Should().BeTrue();

        // Get customer
        Result<CustomerResponse> customerResult = await Poller.WaitAsync(
            TimeSpan.FromSeconds(15),
            async () =>
            {
                var query = new GetCustomerQuery(userResult.Value);

                Result<CustomerResponse> customerResult = await Sender.Send(query);

                return customerResult;
            });

        // Assert
        customerResult.IsSuccess.Should().BeTrue();
        customerResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task RegisterUser_Should_PropagateToAttendanceModule()
    {
        // Register user
        var command = new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Internet.Password(6),
            Faker.Name.FirstName(),
            Faker.Name.LastName());

        Result<Guid> userResult = await Sender.Send(command);

        userResult.IsSuccess.Should().BeTrue();

        // Get attendee
        Result<AttendeeResponse> attendeeResult = await Poller.WaitAsync(
            TimeSpan.FromSeconds(15),
            async () =>
            {
                var query = new GetAttendeeQuery(userResult.Value);

                Result<AttendeeResponse> customerResult = await Sender.Send(query);

                return customerResult;
            });

        // Assert
        attendeeResult.IsSuccess.Should().BeTrue();
        attendeeResult.Value.Should().NotBeNull();
    }
}
