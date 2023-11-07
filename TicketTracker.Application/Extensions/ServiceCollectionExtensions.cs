using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Application.DTO.Ticket;
using TicketTracker.Application.Mappings;
using TicketTracker.Application.Services;

namespace TicketTracker.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITicketService, TicketService>();

            services.AddAutoMapper(typeof(TicketMappingProfile));

            services.AddValidatorsFromAssemblyContaining<TicketDtoValidator>() //registerd type acts for registration for entire assmebly
                .AddFluentValidationAutoValidation()                           //switches default ASP.net validaton to FluentValidation
                .AddFluentValidationClientsideAdapters();                      //Enables FluentValidation on client side
        }
    }
}
