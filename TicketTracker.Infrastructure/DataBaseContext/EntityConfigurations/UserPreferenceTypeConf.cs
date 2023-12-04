using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class UserPreferenceTypeConf : IEntityTypeConfiguration<UserPreferenceType>
    {
        public void Configure(EntityTypeBuilder<UserPreferenceType> builder)
        {
            
        }
    }
}
