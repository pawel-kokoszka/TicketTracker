using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class TeamsUsersEntityConf : IEntityTypeConfiguration<TeamsUsers>
    {
        public void Configure(EntityTypeBuilder<TeamsUsers> builder)
        {
            builder
                .HasKey(tu => new { tu.TeamId, tu.UserId } ); 
        }

    }
}
