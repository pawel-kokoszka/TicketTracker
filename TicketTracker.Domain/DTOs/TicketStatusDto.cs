namespace TicketTracker.Domain.DTOs
{
    public class TicketStatusDto
    {
        //public int Id { get; set; }
        public int StatusId { get; set; }
        public string? Name { get; set; }
        public bool CreatorCanChangeStatus { get; set; }
        public bool AssignedUserCanChangeStatus { get; set; }

    }
}
