using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;
using TicketTracker.Infrastructure.DataBaseContext;
using TicketTracker.Infrastructure.Repositories;
using TicketTracker.Infrastructure.Seeders;

namespace TicketTracker.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TicketTrackerDbContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("TicketTrackerDev")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TicketTrackerDbContext>();

            services.AddScoped<TicketTrackerSeeder>(); 

            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();

            
            services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
            services.AddScoped<IProjectConfigurationRepository, ProjectConfigurationRepository>();
        }

    }
}
