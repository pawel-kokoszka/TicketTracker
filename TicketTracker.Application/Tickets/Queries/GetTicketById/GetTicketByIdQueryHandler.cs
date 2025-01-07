using AutoMapper;
using MediatR;
using TicketTracker.Domain.Interfaces;

namespace TicketTracker.Application.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDetailsDto>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IProjectConfigurationRepository _projectConfigurationRepository;
        private readonly IMapper _mapper;

        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository, IMapper mapper, IProjectConfigurationRepository projectConfigurationRepository)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _projectConfigurationRepository = projectConfigurationRepository;
        }
        public async Task<TicketDetailsDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            //dodaj sprawdzanie uprawnień
            var currentDate = DateTime.UtcNow;

            
            var ticket = await _ticketRepository.GetTicketById(request.TicketId);

            var ticketDetailsDto = _mapper.Map<TicketDetailsDto>(ticket);

            var ticketSla = await _projectConfigurationRepository.GetTicketSlaBySlaId(ticketDetailsDto.TicketSlaConfigurationId);

            var slaTime = new TimeSpan(ticketSla.NumberOfDays,0,ticketSla.NumberOfMinutes,0);

            var createdDate = DateTime.Parse(ticketDetailsDto.DateCreated!);

            var resolutionDate = new DateTime();

            var resolvedDate = new DateTime();

            var timeLeft = new TimeSpan();

            var ticketStatus = ticket.TicketStatusId;

            switch (ticketStatus)
            {
                case 1:
                case 2:
                case 7:
                    resolutionDate = createdDate + slaTime;
                    timeLeft = resolutionDate - currentDate;
                    ticketDetailsDto.ShowResolvedDate = false;
                    ticketDetailsDto.ShowCompletedDate = false;
                    ticketDetailsDto.ShowTicketSlaResolutionDate = true;
                    ticketDetailsDto.ShowTicketSlaTimeLeft = true;
                    ticketDetailsDto.ShowIsOverdue = true;
                    break;
                case 3:
                case 4:
                    resolutionDate = createdDate + slaTime;

                    resolvedDate = (DateTime)ticket.DateSolved!;

                    timeLeft = resolutionDate - resolvedDate;

                    ticketDetailsDto.ShowResolvedDate = true;
                    ticketDetailsDto.ShowCompletedDate = false;
                    ticketDetailsDto.ShowTicketSlaResolutionDate = true;
                    ticketDetailsDto.ShowTicketSlaTimeLeft = true;
                    ticketDetailsDto.ShowIsOverdue = true;
                    break;
                case 5:
                case 6:
                    resolutionDate = createdDate + slaTime;

                    resolvedDate = (DateTime)ticket.DateSolved!;

                    timeLeft = resolutionDate - resolvedDate;

                    ticketDetailsDto.ShowResolvedDate = true;
                    ticketDetailsDto.ShowCompletedDate = true;
                    ticketDetailsDto.ShowTicketSlaResolutionDate = true;
                    ticketDetailsDto.ShowTicketSlaTimeLeft = true;
                    ticketDetailsDto.ShowIsOverdue = true;
                    break;
                case 8:
                    resolutionDate = currentDate;
                    timeLeft = currentDate - currentDate;

                    ticketDetailsDto.ShowResolvedDate = false;
                    ticketDetailsDto.ShowCompletedDate = false;
                    ticketDetailsDto.ShowTicketSlaResolutionDate = false;
                    ticketDetailsDto.ShowTicketSlaTimeLeft = false;
                    ticketDetailsDto.ShowIsOverdue = false;
                    break;
            }

            //spr. status
            //1 jesli open || wip to || reopened 
            //obl. resolution date i wklej ją do TicketSlaResolutionDate i TicketSlaTimeLeft
            // display resolvedDate = false
            // display completedDate = false
            // display TicketSlaTimeLeft = true
            // display TicketSlaResolutionDate = true
            // display IsOverdue = true

            //2 jesli resolved || pending 
            //obl. resolution date i wklej ją do TicketSlaResolutionDate 
            //w TicketDateResolved wklej DateResolved, a w TicketSlaTimeLeft obliczyć czas całkowity od otwarcia do sla, i odjąć od niego czas otawrcia do reslved 
            // display resolvedDate = true
            // display completedDate = false
            // display TicketSlaTimeLeft = true
            // display TicketSlaResolutionDate = true
            // display IsOverdue = true

            //3 jesli completed || closed 
            // to samo co w 2 i:
            // w ticketdDateCompleted wklej DateCompleted, 
            // display resolvedDate = true
            // display completedDate = true               
            // display TicketSlaTimeLeft = true
            // display TicketSlaResolutionDate = true
            // display IsOverdue = true

            //4 jesli rejected:
            // display resolvedDate = false
            // display completedDate = false
            // display TicketSlaTimeLeft = false
            // display TicketSlaResolutionDate = false
            // display IsOverdue = false
            // display TicketSlaTimeLeft = false
            // display TicketSlaResolutionDate = false
            // display IsOverdue = false
            
            ticketDetailsDto.TicketSlaResolutionDate = resolutionDate.ToString("yyyy-MM-dd HH:mm");

            ticketDetailsDto.TicketSlaTimeLeft = $"{timeLeft.Days} days, {timeLeft.Hours} hours, {timeLeft.Minutes} minutes";

            if (timeLeft.Ticks <= 0)
            {
                ticketDetailsDto.IsOverdue = true; 
            }

            return ticketDetailsDto;
        }
    }
}
//Id Name
//1	Opened
//2	Work in Progress
//3	Resolved
//4	Pending
//5	Completed
//6	Closed
//7	Reopened
//8	Rejected