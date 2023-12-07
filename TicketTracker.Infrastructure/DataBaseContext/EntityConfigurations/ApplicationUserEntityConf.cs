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
    public class ApplicationUserEntityConf : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasOne(au => au.UserProfile)
                .WithOne(up => up.ApplicationUser)
                .HasForeignKey<UserProfile>(up => up.UserID);

            builder
                .HasMany(au => au.Ticket)
                .WithOne(t => t.ApplicationUser)
                .HasForeignKey(t => t.CreatedByUserId);

            builder
                .HasMany(au => au.EditedTicket)
                .WithOne(t => t.EditorUser)
                .HasForeignKey(t => t.EditedByUserId);

            builder
                .HasMany(au => au.AssignedTicket)
                .WithOne(t => t.AssignedUser)
                .HasForeignKey(t => t.AssignedToUserId);
        }
    }
}
