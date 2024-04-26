using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTicketWithHistoryById
{
    internal class GetTicketWithHistoryByIdQueryHandler : IRequestHandler<GetTicketWithHistoryByIdQuery, TicketEditSummaryDto>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IMapper _mapper;

        public GetTicketWithHistoryByIdQueryHandler(ITicketRepository ticketRepository, IProjectConfigurationRepository projectConfigurationRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _projectConfigurationRepository = projectConfigurationRepository;
            _mapper = mapper;
        }

        public async Task<TicketEditSummaryDto> Handle(GetTicketWithHistoryByIdQuery request, CancellationToken cancellationToken)
        {
            //dodaj sprawdzanie uprawnień
            var currentDate = DateTime.UtcNow;


            var ticket = await _ticketRepository.GetTicketById(request.TicketId);

            //var ticketDetailsDto = _mapper.Map<TicketDetailsDto>(ticket);
            var ticketDetailsDto = _mapper.Map<TicketEditSummaryDto>(ticket);

            var ticketSla = await _projectConfigurationRepository.GetTicketSlaBySlaId(ticketDetailsDto.TicketSlaConfigurationId);
                        
            var slaTime = new TimeSpan(ticketSla.NumberOfDays, 0, ticketSla.NumberOfMinutes, 0);




            var createdDate = DateTime.Parse(ticketDetailsDto.DateCreated!);

            var resolutionDate = createdDate + slaTime;

            var timeLeft = resolutionDate - currentDate;


            ticketDetailsDto.TicketSlaResolutionDate = resolutionDate.ToString("yyyy-MM-dd HH:mm"); ;

            ticketDetailsDto.TicketSlaTimeLeft = $"{timeLeft.Days} days, {timeLeft.Hours} hours, {timeLeft.Minutes} minutes";

            if (timeLeft.Ticks <= 0)
            {
                ticketDetailsDto.IsOverdue = true;
            }

            var ticketHistoryWithDetails = await _ticketRepository.GetTicketHistoryEntryByLockIdAndTicketId(request.TicketId);

            //ticketHistoryWithDetails.Result;
            ticketDetailsDto.TicketHistory = _mapper.Map<TicketHistoryDto>(ticketHistoryWithDetails);

            var historyPropertyDetails = ticketDetailsDto.TicketHistory.HistoryDetails;

            foreach (var property in historyPropertyDetails!)
            {


                switch (property.TicketPropertyName)
                {
                    case "TicketSlaConfigurationId":
                        property.TicketPropertyDisplayName = "Priority Level:";

                        var slaPair = await _projectConfigurationRepository.GetTicketSlaForSlaList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });
                        
                        property.PropertyOldDisplayValue = slaPair.Find(sla => sla.Id == int.Parse(property.PropertyOldValue!))!.Name;
                        property.PropertyNewDisplayValue = slaPair.Find(sla => sla.Id == int.Parse(property.PropertyNewValue!))!.Name;
                        break;

                    case "Description":
                        property.TicketPropertyDisplayName = "Description";

                        property.PropertyOldDisplayValue = property.PropertyOldValue!.ToString();                        
                        property.PropertyNewDisplayValue = property.PropertyNewValue!.ToString(); 
                        break;

                    case "ShortDescription":
                        property.TicketPropertyDisplayName = "Short Description";

                        property.PropertyOldDisplayValue = property.PropertyOldValue!.ToString();
                        property.PropertyNewDisplayValue = property.PropertyNewValue!.ToString();
                        break;

                    case "TicketStatusId":
                        property.TicketPropertyDisplayName = "Ticket Status:";

                        var statusPair = await _projectConfigurationRepository.GetTicketStatusesForIdList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });

                        property.PropertyOldDisplayValue = statusPair.Find(status => status.Id == int.Parse(property.PropertyOldValue!))!.Name;
                        property.PropertyNewDisplayValue = statusPair.Find(status => status.Id == int.Parse(property.PropertyNewValue!))!.Name;
                        break;

                    case "TicketServiceId":
                        property.TicketPropertyDisplayName = "Service:";

                        var servicePair = await _projectConfigurationRepository.GetTicketServicesForIdList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });

                        property.PropertyOldDisplayValue = servicePair.Find(service => service.Id == int.Parse(property.PropertyOldValue!))!.ServiceName;
                        property.PropertyNewDisplayValue = servicePair.Find(service => service.Id == int.Parse(property.PropertyNewValue!))!.ServiceName;
                        break;

                    case "TicketSubServiceId":
                        property.TicketPropertyDisplayName = "Sub Service:";

                        var subServicePair = await _projectConfigurationRepository.GetTicketSubServicesForIdList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });

                        property.PropertyOldDisplayValue = subServicePair.Find(subService => subService.Id == int.Parse(property.PropertyOldValue!))!.SubServiceName;
                        property.PropertyNewDisplayValue = subServicePair.Find(subService => subService.Id == int.Parse(property.PropertyNewValue!))!.SubServiceName;
                        break;

                    case "AssignedTeamId":
                        property.TicketPropertyDisplayName = "Team:";

                        var teamPair = await _projectConfigurationRepository.GetTeamsForIdList(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });

                        property.PropertyOldDisplayValue = teamPair.Find(team => team.Id == int.Parse(property.PropertyOldValue!))!.Name;
                        property.PropertyNewDisplayValue = teamPair.Find(team => team.Id == int.Parse(property.PropertyNewValue!))!.Name;
                        break;

                    case "AssignedUserId":
                        property.TicketPropertyDisplayName = "Assigned User:";

                        var userPair = await _projectConfigurationRepository.GetUsersForIdList(new List<string>() { property.PropertyOldValue!, property.PropertyNewValue! });

                        if (property.PropertyOldValue == null )
                        {
                            property.PropertyOldDisplayValue = "Ticket Manager";
                        }
                        else
                        {
                            property.PropertyOldDisplayValue = userPair.Find(user => user.Id == (property.PropertyOldValue))!.Email;

                        }

                        property.PropertyNewDisplayValue = userPair.Find(user => user.Id == (property.PropertyNewValue))!.Email;
                        break;
                        

                }

            }
            
            return ticketDetailsDto;
        }
    }
}
