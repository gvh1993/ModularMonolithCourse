using Bogus;
using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Customers.CreateCustomer;
using Evently.Modules.Ticketing.Application.Events.CreateEvent;
using FluentAssertions;
using MediatR;

namespace Evently.Modules.Ticketing.IntegrationTests.Abstractions;

internal static class CommandHelpers
{
    internal static async Task<Guid> CreateCustomerAsync(this ISender sender, Guid customerId)
    {
        var faker = new Faker();
        Result result = await sender.Send(
            new CreateCustomerCommand(
                customerId,
                faker.Internet.Email(),
                faker.Person.FirstName,
                faker.Person.LastName));

        result.IsSuccess.Should().BeTrue();

        return customerId;
    }

    internal static async Task CreateEventWithTicketTypeAsync(
        this ISender sender,
        Guid eventId,
        Guid ticketTypeId,
        decimal quantity)
    {
        var faker = new Faker();

        var ticketType = new CreateEventCommand.TicketTypeRequest(
            ticketTypeId,
            eventId,
            faker.Music.Genre(),
            faker.Random.Decimal(),
            "USD",
            quantity);

        Result result = await sender.Send(new CreateEventCommand(
            eventId,
            faker.Music.Genre(),
            faker.Music.Genre(),
            faker.Address.FullAddress(),
            DateTime.UtcNow,
            null,
            [ticketType]));

        result.IsSuccess.Should().BeTrue();
    }
}
