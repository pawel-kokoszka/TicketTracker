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
    public class EnvironmentEntityConf : IEntityTypeConfiguration<Domain.Entities.Environment>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Environment> builder)
        {
            builder
                .HasOne(e => e.ProjectConfiguration)
                .WithOne(pc => pc.Environment)
                .HasForeignKey<ProjectConfiguration>(pc => pc.EnvironmentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
