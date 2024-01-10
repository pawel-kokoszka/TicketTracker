namespace TicketTracker.Domain.Entities
{
    public class TicketStatus
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        //ef
        public List<Ticket>? Tickets { get; set; }
        public List<TicketFlowConfiguration>? TicketFlowConfigurations { get; set; }

    }
}
