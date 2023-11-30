using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext
{
    public class TicketTrackerDbContext : IdentityDbContext
    {
        public TicketTrackerDbContext(DbContextOptions<TicketTrackerDbContext> options) : base(options)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TicketPriority> TicketPriorities { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticket>()
                .HasMany(t => t.Comments)
                .WithOne(c => c.Ticket)
                .HasForeignKey(t => t.TicketId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Ticket>()
                .HasOne(t => t.TicketType)
                .WithOne(tt => tt.Ticket)
                .HasForeignKey<Ticket>(t => t.TypeId);

                //.HasForeignKey(t => t.)
                //.OnDelete(DeleteBehavior.NoAction);


        }
    }
}
