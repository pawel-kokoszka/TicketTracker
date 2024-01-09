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
    internal class TicketStatusEntityConf : IEntityTypeConfiguration<TicketStatus>
    {
        public void Configure(EntityTypeBuilder<TicketStatus> builder)
        {
            builder
                .HasMany(ts => ts.Tickets)
                .WithOne(t => t.TicketStatuses)
                .HasForeignKey(t => t.TicketStatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ts => ts.TicketFlowConfigurations)
                .WithOne(t => t.TicketStatuses)
                .HasForeignKey(tfc => tfc.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ts => ts.TicketFlowConfigurations)
                .WithOne(t => t.TicketStatuses)
                .HasForeignKey(tfc => tfc.NextStatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
