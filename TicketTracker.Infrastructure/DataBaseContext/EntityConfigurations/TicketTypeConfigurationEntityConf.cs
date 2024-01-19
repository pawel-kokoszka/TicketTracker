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
                .HasMany(ttc => ttc.TicketPrioritiesConfigurations)
                .WithOne(tpg => tpg.TicketTypeConfiguration)
                .HasForeignKey(tpg => tpg.TicketTypeConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ttc => ttc.TicketFlowConfigurations)
                .WithOne(tfc => tfc.TicketTypeConfigurations)
                .HasForeignKey(tfc => tfc.TicketTypeConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);



            //do refactoru
            builder
                .HasMany(ttc => ttc.TicketServiceConfigurations)
                .WithOne(tsc => tsc.TicketTypeConfiguration)
                .HasForeignKey(tsc => tsc.TicketTypeConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ttc => ttc.TicketTypeTeamAssignRules )
                .WithOne(ttasr => ttasr.TicketTypeConfiguration)
                .HasForeignKey(ttasr => ttasr.TicketTypeConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);
                                                



        }
    }
}
