using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TicketPriorityEntityConf : IEntityTypeConfiguration<TicketPriority>
    {
        public void Configure(EntityTypeBuilder<TicketPriority> builder)
        {
            builder
                .HasMany(tp => tp.Tickets)
                .WithOne(t => t.TicketPriority)
                .HasForeignKey(t => t.TicketPriorityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
              .HasMany(tp => tp.TicketPrioritiesConfigurations)
              .WithOne(t => t.TicketPriority)
              .HasForeignKey(t => t.TicketPriorityId)
              .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
