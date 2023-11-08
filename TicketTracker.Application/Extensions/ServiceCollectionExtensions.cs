using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using TicketTracker.Application.Tickets.Commands.CreateTicket;
using TicketTracker.Application.Mappings;
using MediatR;


namespace TicketTracker.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateTicketCommand)); //registerd type acts for registration for entire assmebly

            services.AddAutoMapper(typeof(TicketMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CreateTicketCommandValidator>() //registerd type acts for registration for entire assmebly
                .AddFluentValidationAutoValidation()                           //switches default ASP.net validaton to FluentValidation
                .AddFluentValidationClientsideAdapters();                      //Enables FluentValidation on client side
            
        }
    }
}
