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
    public class TeamTypeEntityConf : IEntityTypeConfiguration<TeamType>
    {
        public void Configure(EntityTypeBuilder<TeamType> builder)
        {
            builder
                 .HasMany(tt => tt.UserTeamConfigurations)       
                 .WithOne(utc => utc.TeamType)
                 .HasForeignKey(utc => utc.TeamTypeId)
                 .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
