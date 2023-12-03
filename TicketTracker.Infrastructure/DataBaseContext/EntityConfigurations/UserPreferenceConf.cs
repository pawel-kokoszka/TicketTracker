using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class UserPreferenceConf : IEntityTypeConfiguration<UserPreference>
    {
        public void Configure(EntityTypeBuilder<UserPreference> builder)
        {
            builder
                .HasKey(up => new { up.UserProfileId, up.PreferenceTypeId });


            builder
                .HasOne(up => up.UserProfile)
                .WithOne(uprofile => uprofile.UserPreference)
                .HasForeignKey<UserPreference>(up => up.UserProfileId);

            builder
                .HasOne(up => up.PreferenceType)
                .WithOne(pt => pt.UserPreference)
                .HasForeignKey<UserPreference>(up => up.PreferenceTypeId);


        }
    }
}
