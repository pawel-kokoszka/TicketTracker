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
    internal class TicketFlowConfigurationEntityConf : IEntityTypeConfiguration<TicketFlowConfiguration>
    {
        public void Configure(EntityTypeBuilder<TicketFlowConfiguration> builder)
        {
            
            
        }
    }
}
