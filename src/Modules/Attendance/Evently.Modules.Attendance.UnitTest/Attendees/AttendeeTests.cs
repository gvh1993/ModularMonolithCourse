using Evently.Common.Domain;
using Evently.Modules.Attendance.Domain.Attendees;
using Evently.Modules.Attendance.Domain.Events;
using Evently.Modules.Attendance.Domain.Tickets;
using Evently.Modules.Attendance.UnitTests.Abstractions;
using FluentAssertions;

namespace Evently.Modules.Attendance.UnitTests.Attendees;

public class AttendeeTests : BaseTest
{
    [Fact]
    public void CheckIn_ShouldReturnFailure_WhenTicketIsNotValid()
    {
        //Arrange
        var attendee = Attendee.Create(
            Guid.NewGuid(),
            Faker.Internet.Email(),
            Faker.Person.FirstName,
            Faker.Person.LastName);

        var ticketAttendee = Attendee.Create(
            Guid.NewGuid(), 
            Faker.Internet.Email(), 
            Faker.Person.FirstName, 
            Faker.Person.LastName);

        DateTime startsAtUtc = DateTime.UtcNow;

        var @event = Event.Create(
            Guid.NewGuid(), 
            Faker.Music.Genre(), 
            Faker.Music.Genre(), 
            Faker.Address.StreetName(), 
            startsAtUtc, null);

        var ticket = Ticket.Create(
            Guid.NewGuid(),
            ticketAttendee,
            @event, 
            Faker.Random.String());

        //Act
        Result checkInAttendee = attendee.CheckIn(ticket);

        //Assert
        InvalidCheckInAttemptedDomainEvent domainEvent =
            AssertDomainEventWasPublished<InvalidCheckInAttemptedDomainEvent>(attendee);

        domainEvent.AttendeeId.Should().Be(attendee.Id);

        checkInAttendee.Error.Should().Be(TicketErrors.InvalidCheckIn);
    }

    [Fact]
    public void CheckIn_ShouldReturnFailure_WhenTicketAlreadyUsed()
    {
        //Arrange
        var attendee = Attendee.Create(
            Guid.NewGuid(), 
            Faker.Internet.Email(),
            Faker.Person.FirstName,
            Faker.Person.LastName);

        DateTime startsAtUtc = DateTime.UtcNow;

        var @event = Event.Create(
            Guid.NewGuid(), 
            Faker.Music.Genre(), 
            Faker.Music.Genre(), 
            Faker.Address.StreetName(), 
            startsAtUtc, null);

        var ticket = Ticket.Create(
            Guid.NewGuid(),
            attendee,
            @event, 
            Faker.Random.String());

        ticket.MarkAsUsed();

        //Act
        Result checkInAttendee = attendee.CheckIn(ticket);

        //Assert
        DuplicateCheckInAttemptedDomainEvent domainEvent =
            AssertDomainEventWasPublished<DuplicateCheckInAttemptedDomainEvent>(attendee);

        domainEvent.AttendeeId.Should().Be(attendee.Id);

        checkInAttendee.Error.Should().Be(TicketErrors.DuplicateCheckIn);
    }

    [Fact]
    public void CheckIn_ShouldRaiseDomainEvent_WhenSuccessfullyCheckedIn()
    {
        //Arrange
        var attendee = Attendee.Create(
            Guid.NewGuid(), 
            Faker.Internet.Email(), 
            Faker.Person.FirstName, 
            Faker.Person.LastName);

        DateTime startsAtUtc = DateTime.UtcNow;

        var @event = Event.Create(
            Guid.NewGuid(), 
            Faker.Music.Genre(),
            Faker.Music.Genre(),
            Faker.Address.StreetName(),
            startsAtUtc, null);

        var ticket = Ticket.Create(
            Guid.NewGuid(),
            attendee, 
            @event, 
            Faker.Random.String());

        //Act
        attendee.CheckIn(ticket);

        //Assert
        AttendeeCheckedInDomainEvent domainEvent =
            AssertDomainEventWasPublished<AttendeeCheckedInDomainEvent>(attendee);

        domainEvent.AttendeeId.Should().Be(attendee.Id);
    }
}
