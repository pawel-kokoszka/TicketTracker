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

            var resolutionDate = createdDate + slaTime;
            
            var timeLeft =  resolutionDate - currentDate;


            ticketDetailsDto.TicketSlaResolutionDate = resolutionDate.ToString("yyyy-MM-dd HH:mm"); ;

            ticketDetailsDto.TicketSlaTimeLeft = $"{timeLeft.Days} days, {timeLeft.Hours} hours, {timeLeft.Minutes} minutes";

            if (timeLeft.Ticks <= 0)
            {
                ticketDetailsDto.IsOverdue = true; 
            }

            return ticketDetailsDto;
        }
    }
}
