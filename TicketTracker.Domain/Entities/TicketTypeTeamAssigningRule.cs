﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketTypeTeamAssigningRule
    {
        public int Id { get; set; } 
        public int TicketTypeConfigurationId { get; set; }
        public int AssigningTeamId { get; set; }    
        public int AssignedTeamId { get; set; }

        //ef
        public Team? Team { get; set; } 
        public TicketTypeConfiguration? TicketTypeConfiguration { get; set; }


    }
}
