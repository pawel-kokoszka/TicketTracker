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
    public class TicketServicesConfigurationEntityConf : IEntityTypeConfiguration<TicketServicesConfiguration>
    {
        public void Configure(EntityTypeBuilder<TicketServicesConfiguration> builder)
        {
            builder
                .HasKey(tsc => new { tsc.TicketTypeConfigurationId, tsc.ServiceId});

        }
    }
}
