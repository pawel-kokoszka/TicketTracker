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
                .HasOne(t => t.TicketType)
                .WithOne(tt => tt.Ticket)
                .HasForeignKey<Ticket>(t => t.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(t => t.ProjectConfiguration)
                .WithOne(pc => pc.Ticket)
                .HasForeignKey<Ticket>(t => t.ProjectConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);
            
            
            
            
            
            
            
            
            
            
            
            
            //builder.Property(t => t.Description)
            //    .HasMaxLength(1000);
            
            //builder.Property(t => t.ShortDescription)
            //    .HasMaxLength(100);


        }
    }
}
