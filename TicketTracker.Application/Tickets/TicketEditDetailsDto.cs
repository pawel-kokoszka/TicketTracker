namespace TicketTracker.Application.Tickets
{
    public class TicketEditDetailsDto : TicketDetailsDto  
    {

        public List<TicketSlaDto>? TicketSlaDtos { get; set; }
        public List<TicketStatusDto>? TicketStatusDtos { get; set; }

        public List<TicketTracker.Domain.DTOs.TeamDto>? UsersToAssign { get; set; }



        //public List<Comment>? Comments { get; set; }
    }
        
}
