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
    public class EnvironmentTypeEntityConf : IEntityTypeConfiguration<EnvironmentType>
    {
        public void Configure(EntityTypeBuilder<EnvironmentType> builder)
        {
            builder
                .HasMany(et => et.Environment)
                .WithOne(e => e.EnvironmentType)
                .HasForeignKey(et => et.EnvironmentTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
