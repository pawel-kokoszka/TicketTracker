﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TicketSlaGonfigurationEntityConf : IEntityTypeConfiguration<TicketSlaConfiguration>
    {
        public void Configure(EntityTypeBuilder<TicketSlaConfiguration> builder)
        {
            builder
                .HasKey(tpg => tpg.Id);

            
            builder
                .HasMany(t => t.Tickets)
                .WithOne(tsg => tsg.TicketSlaConfigurations)
                .HasForeignKey(t => t.TicketSlaConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);
        


    }
    }
}
