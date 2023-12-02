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

            //dodaj tablę TicketSlaConfiguration
        }
    }
}
