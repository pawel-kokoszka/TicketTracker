using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TeamTypeEntityConf : IEntityTypeConfiguration<TeamType>
    {
        public void Configure(EntityTypeBuilder<TeamType> builder)
        {
            builder
                 .HasMany(tt => tt.Teams)       
                 .WithOne(t => t.TeamType)
                 .HasForeignKey(t => t.TeamTypeId)
                 .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
