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
    internal class TicketTypeConfigurationEntityConf : IEntityTypeConfiguration<TicketTypeConfiguration>
    {
        public void Configure(EntityTypeBuilder<TicketTypeConfiguration> builder)
        {
            builder
                .HasOne(ttc => ttc.ProjectConfiguration)
                .WithOne(pc => pc.TicketTypeConfiguration)
                .HasForeignKey<TicketTypeConfiguration>(ttc => ttc.ProjectConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasOne(ttc => ttc.TicketType)
                .WithOne(tt => tt.TicTicketTypeConfiguration)
                .HasForeignKey<TicketTypeConfiguration>(ttc => ttc.TicketTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(ttc => ttc.TicketPrioritiesGonfiguration)
                .WithOne(tpg => tpg.TicketTypeConfiguration)
                .HasForeignKey<TicketPrioritiesGonfiguration>(tpg => tpg.TicketTypeConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(ttc => ttc.TicketServicesConfiguration)
                .WithOne(tsc => tsc.TicketTypeConfiguration)
                .HasForeignKey<TicketServicesConfiguration>(tsc => tsc.TicketTypeConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
