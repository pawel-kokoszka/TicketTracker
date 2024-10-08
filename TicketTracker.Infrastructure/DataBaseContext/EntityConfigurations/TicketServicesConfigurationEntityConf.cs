﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TicketServicesConfigurationEntityConf : IEntityTypeConfiguration<TicketServiceConfiguration>
    {
        public void Configure(EntityTypeBuilder<TicketServiceConfiguration> builder)
        {
            builder
                .HasKey(tsc => new { tsc.TicketTypeConfigurationId, tsc.TicketServiceId});



        }
    }
}
