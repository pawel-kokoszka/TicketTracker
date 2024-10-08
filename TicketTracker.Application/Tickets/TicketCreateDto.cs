﻿namespace TicketTracker.Application.Tickets
{
    public class TicketCreateDto
    {
        public string? Description { get; set; }
        public string? ShortDescription { get; set; }


        public int ProjectConfigurationId { get; set; }
        public int TypeId { get; set; }
        public int TicketTypeConfigurationId { get; set; }

        public int TicketSlaConfigurationId { get; set; }
        public int TicketStatusId { get; set; }



        public string? CreatedByUserId { get; set; }
        public int AssigningTeamId { get; set; }


        public int TicketServiceId { get; set; }
        public int TicketSubServiceId { get; set; }


   
        public int? AssignedTeamId { get; set; }
        public string? AssignedUserId { get; set; }

        public bool IsEditable { get; set; }
    }
}
