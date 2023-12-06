using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;


namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class UserProfileConf : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            //builder
            //    .HasOne(up => up.IdentityUser)
            //    .WithOne(u => u.)
        }
    }
}
