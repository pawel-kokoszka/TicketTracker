﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TicketTypeTeamAssignRuleEntityConf : IEntityTypeConfiguration<TicketTypeTeamAssigningRule>
    {
        public void Configure(EntityTypeBuilder<TicketTypeTeamAssigningRule> builder)
        {
            
        }
    }
}
