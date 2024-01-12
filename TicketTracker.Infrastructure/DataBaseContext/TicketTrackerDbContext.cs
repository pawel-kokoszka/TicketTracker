using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection.Emit;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Infrastructure.DataBaseContext
{
    public class TicketTrackerDbContext : IdentityDbContext<ApplicationUser>
    {
        public TicketTrackerDbContext(DbContextOptions<TicketTrackerDbContext> options) : base(options)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }



        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectConfiguration> ProjectConfigurations { get; set; }

        public DbSet<TicketServiceConfiguration> TicketServicesConfigurations { get; set; }
        public DbSet<TicketService> TicketServices { get; set; }
        
        
        public DbSet<TicketSlaConfiguration> TicketSlaConfigurations { get; set; }
        
        public DbSet<TicketTypeConfiguration> TicketTypeConfigurations { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<TicketFlowConfiguration> TicketFlowConfigurations { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }

        public DbSet<Domain.Entities.Environment> Environments { get; set; }
        public DbSet<Domain.Entities.EnvironmentType> EnvironmentTypes { get; set; }

        
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserPreference> UserPreferences { get; set; }
        public DbSet<UserPreferenceType> UserPreferenceTypes { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
                      

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
        }

    }
}
