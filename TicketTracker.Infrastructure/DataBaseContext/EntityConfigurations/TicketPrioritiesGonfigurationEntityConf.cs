using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TicketPrioritiesGonfigurationEntityConf : IEntityTypeConfiguration<TicketPrioritiesConfiguration>
    {
        public void Configure(EntityTypeBuilder<TicketPrioritiesConfiguration> builder)
        {
            builder
                .HasKey(tpg => tpg.Id);

            
            
            

        }
    }
}
