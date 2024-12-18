using Evently.Modules.Attendance.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evently.Modules.Attendance.Infrastructure.Events;

internal sealed class EventStatisticsConfiguration : IEntityTypeConfiguration<EventStatistics>
{
    public void Configure(EntityTypeBuilder<EventStatistics> builder)
    {
        builder.ToTable("event_statistics");

        builder.HasKey(es => es.EventId);

        builder.Property(es => es.EventId).ValueGeneratedNever();
    }
}
