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
    public class TicketSlaConfigurationEntityConfiguration : IEntityTypeConfiguration<TicketSlaConfiguration>
    {
        public void Configure(EntityTypeBuilder<TicketSlaConfiguration> builder)
        {
            //builder
            //    .HasKey(tsc => tsc.Id);



            builder
                .HasOne(tsc => tsc.TicketSla)
                .WithOne(ts => ts.TicketSlaConfiguration)
                .HasForeignKey<TicketSlaConfiguration>(tsc => tsc.TicketSlaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
