using Microsoft.EntityFrameworkCore;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    internal class TicketServiceEntityConf : IEntityTypeConfiguration<TicketService>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketService> builder)
        {
            builder
               .HasOne(ts => ts.TicketServicesConfiguration)
               .WithOne(tsc => tsc.TicketService)
               .HasForeignKey<TicketServicesConfiguration>(tsc => tsc.ServiceId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
