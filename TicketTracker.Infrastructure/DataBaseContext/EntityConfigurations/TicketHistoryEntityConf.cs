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
    internal class TicketHistoryEntityConf : IEntityTypeConfiguration<TicketHistory>
    {
        public void Configure(EntityTypeBuilder<TicketHistory> builder)
        {
            builder
                .HasMany(th => th.HistoryDetails)
                .WithOne(thd => thd.TicketHistory)
                .HasForeignKey(thd => thd.TicketHistoryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
