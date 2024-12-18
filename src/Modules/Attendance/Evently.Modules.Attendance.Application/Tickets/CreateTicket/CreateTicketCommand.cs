using Evently.Common.Application.Messaging;

namespace Evently.Modules.Attendance.Application.Tickets.CreateTicket;

public sealed record CreateTicketCommand(Guid TicketId, Guid AttendeeId, Guid EventId, string Code) : ICommand;
