using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TicketPrioritiesGonfigurationEntityConfiguration : IEntityTypeConfiguration<TicketPrioritiesGonfiguration>
    {
        public void Configure(EntityTypeBuilder<TicketPrioritiesGonfiguration> builder)
        {
            builder
                .HasKey(tpg => new { tpg.TicketTypeConfigurationId, tpg.TicketPriorityId});

            builder
                .HasOne(tpg => tpg.TicketPriority)
                .WithOne(tp => tp.TicketPrioritiesGonfiguration)
                .HasForeignKey<TicketPrioritiesGonfiguration>(tpg => tpg.TicketPriorityId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder
                .HasOne(tpg => tpg.TicketSlaConfiguration)
                .WithOne(tsc => tsc.TicketPrioritiesGonfiguration)
                .HasForeignKey<TicketPrioritiesGonfiguration>(tpg => tpg.TicketSlaConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
