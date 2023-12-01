﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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



        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectConfiguration> ProjectConfigurations { get; set; }
        
        //public DbSet<TicketSlaConfiguration> TicketSlaConfigurations { get; set; }
        //public DbSet<TicketSla> TicketSLAs { get; set; }
        
        //public DbSet<TicketStatesGonfiguration> TicketStatesGonfigurations { get; set; }
        //public DbSet<TicketState> TicketStates { get; set; }
        
        //public DbSet<TicketServicesConfiguration> TicketServicesConfigurations { get; set; }
        //public DbSet<TicketService> TicketServices { get; set; }
        
        public DbSet<TicketPriority> TicketPriorities { get; set; }
        //public DbSet<TicketPrioritiesGonfiguration> TicketPrioritiesGonfiguration { get; set; }
        
        //public DbSet<TicketTypeConfiguration> TicketTypeConfigurations { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        public DbSet<Domain.Entities.Environment> Environments { get; set; }
        public DbSet<Domain.Entities.EnvironmentType> EnvironmentTypes { get; set; }

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
                .HasForeignKey<Ticket>(t => t.TypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Ticket>()
                .HasOne(t => t.ProjectConfiguration)
                .WithOne(pc => pc.Ticket)
                .HasForeignKey<Ticket>(t => t.ProjectConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);

            //.HasForeignKey(t => t.)
            //.OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Project>()
                .HasOne(t => t.ProjectConfiguration)
                .WithOne(pc => pc.Project)
                .HasForeignKey<ProjectConfiguration>(pc => pc.ProjectId)
                .OnDelete(DeleteBehavior.NoAction);


        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
        }
    }
}
