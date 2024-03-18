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
    public class TicketEntityConf : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
                .HasMany(t => t.Comments)
                .WithOne(c => c.Ticket)
                .HasForeignKey(t => t.TicketId)
                .OnDelete(DeleteBehavior.NoAction);

             builder
                .HasMany(t => t.TicketHistory)
                .WithOne(c => c.Ticket)
                .HasForeignKey(t => t.TicketId)
                .OnDelete(DeleteBehavior.NoAction);





        }
    }
}
