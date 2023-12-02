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
    public class TicketStatesGonfigurationEntityConf : IEntityTypeConfiguration<TicketStatesGonfiguration>
    {
        public void Configure(EntityTypeBuilder<TicketStatesGonfiguration> builder)
        {
            builder
                .HasKey(tsg => new { tsg.TicketTypeConfigurationId, tsg.TicketStateId });

            builder
                .HasOne(tsg => tsg.TicketState)
                .WithOne(tt => tt.TicketStatesGonfiguration)
                .HasForeignKey<TicketStatesGonfiguration>(tsg => tsg.TicketStateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasOne(tsg => tsg.TicketTypeConfiguration)
               .WithOne(ttc => ttc.TicketStatesGonfiguration)
               .HasForeignKey<TicketStatesGonfiguration>(tsg => tsg.TicketTypeConfigurationId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
