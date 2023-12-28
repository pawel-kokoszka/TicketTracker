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
    public class TicketSlaEntityConf : IEntityTypeConfiguration<TicketSla>
    {
        public void Configure(EntityTypeBuilder<TicketSla> builder)
        {
            builder
              .HasMany(ts => ts.TicketPrioritiesConfigurations)
              .WithOne(t => t.TicketSla)
              .HasForeignKey(t => t.TicketSlaId)
              .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(ts => ts.Tickets)
                .WithOne(t => t.TicketSla)
                .HasForeignKey(t => t.TicketPriorityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
