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
    internal class TicketSubServiceEntityConf : IEntityTypeConfiguration<TicketSubService>
    {
        public void Configure(EntityTypeBuilder<TicketSubService> builder)
        {
            builder
                .HasMany(tss => tss.Tickets)
                .WithOne(t => t.TicketSubService)
                .HasForeignKey(t => t.TicketSubServiceId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
