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
    public class TeamRoleTypeEntityConf : IEntityTypeConfiguration<TeamRoleType>
    {
        public void Configure(EntityTypeBuilder<TeamRoleType> builder)
        {
            builder
                 .HasMany(trt => trt.TeamsRoles)
                 .WithOne(tr => tr.TeamRoleType)
                 .HasForeignKey(tr => tr.RoleId)
                 .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
