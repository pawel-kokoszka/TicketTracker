using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TicketTypeTeamAssignRuleEntityConf : IEntityTypeConfiguration<TicketTypeTeamAssignRule>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TicketTypeTeamAssignRule> builder)
        {
            
        }
    }
}
