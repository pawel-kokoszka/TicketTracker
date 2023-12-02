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
    internal class TicketTypeConfigurationEntityConfiguration : IEntityTypeConfiguration<TicketTypeConfiguration>
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
        }
    }
}
