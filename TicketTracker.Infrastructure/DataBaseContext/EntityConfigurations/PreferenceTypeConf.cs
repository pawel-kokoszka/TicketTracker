using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext.EntityConfigurations
{
    public class PreferenceTypeConf : IEntityTypeConfiguration<PreferenceType>
    {
        public void Configure(EntityTypeBuilder<PreferenceType> builder)
        {
            throw new NotImplementedException();
        }
    }
}
