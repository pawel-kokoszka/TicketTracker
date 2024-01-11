using Microsoft.EntityFrameworkCore;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    internal class TicketServiceEntityConf : IEntityTypeConfiguration<TicketService>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketService> builder)
        {
            builder
               .HasMany(ts => ts.TicketServiceConfigurations)
               .WithOne(tsc => tsc.TicketService)
               .HasForeignKey(tsc => tsc.TicketServiceId)
               .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasMany(ts => ts.TicketSubServices)
               .WithOne(tss => tss.TicketService)
               .HasForeignKey(tss => tss.TicketServiceId)
               .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasMany(ts => ts.Tickets)
               .WithOne(t => t.TicketService)
               .HasForeignKey(t => t.TicketServiceId)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
