using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class ProjectConfigurationEntityConf : IEntityTypeConfiguration<ProjectConfiguration>
    {
        public void Configure(EntityTypeBuilder<ProjectConfiguration> builder)
        {
            builder
                .HasMany(pc => pc.Tickets)
                .WithOne(t => t.ProjectConfiguration)
                .HasForeignKey(t => t.ProjectConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(pc => pc.TicketTypeConfiguration)
                .WithOne(ttc => ttc.ProjectConfiguration)
                .HasForeignKey(ttc => ttc.ProjectConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
