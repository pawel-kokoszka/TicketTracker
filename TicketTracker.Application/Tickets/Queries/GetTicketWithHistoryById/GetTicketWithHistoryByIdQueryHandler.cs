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
                        //repository get slaOld albo oba pola na raz
                        var slaPair = await _projectConfigurationRepository.GetTicketSlaBySlaIdPair(new List<int>() { int.Parse(property.PropertyOldValue!), int.Parse(property.PropertyNewValue!) });
                        //przypisz nazwę do pola slaOld 
                        property.PropertyOldDisplayValue = slaPair.Find(sla => sla.Id == int.Parse(property.PropertyOldValue!))!.Name;
                        //przypisz nazwę do pola slaNew 
                        property.PropertyNewDisplayValue = slaPair.Find(sla => sla.Id == int.Parse(property.PropertyNewValue!))!.Name;

                        break;


                }

            }

            //trzeba dorobić mapowanie ticketHistoryWithDetails( TicketHistory ) na TicketEditSummaryDto 
            //var ticketEditSummaryDto = _mapper.Map<TicketEditSummaryDto>(ticketHistoryWithDetails);


            return ticketDetailsDto;
        }
    }
}
