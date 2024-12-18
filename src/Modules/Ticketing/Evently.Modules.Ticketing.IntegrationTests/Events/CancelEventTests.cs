using Evently.Common.Domain;
using Evently.Modules.Ticketing.Application.Events.CancelEvent;
using Evently.Modules.Ticketing.Domain.Events;
using Evently.Modules.Ticketing.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Ticketing.IntegrationTests.Events;

public class CancelEventTests : BaseIntegrationTest
{
    private const decimal Quantity = 10;

    public CancelEventTests(IntegrationTestWebAppFactory factory) 
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnFailure_WhenEventDoesNotExist()
    {
        //Arrange
        var eventId = Guid.NewGuid();

        var command = new CancelEventCommand(eventId);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.Error.Should().Be(EventErrors.NotFound(command.EventId));
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenEventIsCanceled()
    {
        //Arrange
        var eventId = Guid.NewGuid();
        var ticketTypeId = Guid.NewGuid();

        await Sender.CreateEventWithTicketTypeAsync(eventId, ticketTypeId, Quantity);

        var command = new CancelEventCommand(eventId);

        //Act
        Result result = await Sender.Send(command);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }
}
