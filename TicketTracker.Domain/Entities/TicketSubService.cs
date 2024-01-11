namespace TicketTracker.Domain.Entities
{
    public class TicketSubService
    {
        public int Id { get; set; }
        public int TicketServiceId { get; set; }
        public string? SubServiceName { get; set; }
        public int DisplayOrderValue { get; set; }

        //ef
        public TicketService? TicketService { get; set; }
        public List<Ticket>? Tickets { get; set; }  
    }
}