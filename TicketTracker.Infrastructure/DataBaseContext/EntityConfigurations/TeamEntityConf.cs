using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TeamEntityConf : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder
                .HasMany(t => t.TeamsUsers)
                .WithOne(tu => tu.Team)
                .HasForeignKey(tu => tu.TeamId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(t => t.TicketTypeTeamAssigningRules)
                .WithOne(ttt => ttt.Team)
                .HasForeignKey(ttt => ttt.AssigningTeamId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(t => t.TicketTypeTeamAssigningRules)
                .WithOne(ttt => ttt.Team)
                .HasForeignKey(ttt => ttt.AssignedTeamId)
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
