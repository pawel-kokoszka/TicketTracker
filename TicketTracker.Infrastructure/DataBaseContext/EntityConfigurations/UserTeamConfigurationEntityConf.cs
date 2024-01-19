using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class UserTeamConfigurationEntityConf : IEntityTypeConfiguration<UserTeamConfiguration>
    {
        public void Configure(EntityTypeBuilder<UserTeamConfiguration> builder)
        {
            builder
                .HasMany(utc => utc.TicketTypeTeamAssignRules)
                .WithOne(tttar => tttar.AssigningUserTeamConfigurations)
                .HasForeignKey(tttar => tttar.AssigningTeamId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(utc => utc.TicketTypeTeamAssignRules)
                .WithOne(tttar => tttar.AssigningUserTeamConfigurations)
                .HasForeignKey(tttar => tttar.AssignedTeamId)
                .OnDelete(DeleteBehavior.NoAction);
        }

    }
}
