namespace TicketTracker.Application.Tickets
{
    public class TicketCreateDto
    {
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }


        public int ProjectConfigurationId { get; set; }
        public int TypeId { get; set; }
        public int TicketSlaConfigurationId { get; set; }


        public string? AssignedToUserId { get; set; }

        //do dodania przy procesowaniu biletów 
        //public int TicketStateId { get; set; }
        //public int TicketServiceId { get; set; }
    }
}
