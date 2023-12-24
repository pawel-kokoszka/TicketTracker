﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Domain.Entities
{
    public class TicketTypeConfiguration
    {
        public int Id { get; set; }
        public int TicketTypeId { get; set; }
        public int ProjectConfigurationId { get; set; }

        //EF 
        public ProjectConfiguration? ProjectConfiguration { get; set; }
        public TicketType? TicketType { get; set; }
        public List<TicketPrioritiesConfiguration>? TicketPrioritiesConfigurations { get; set; }
        public TicketStatesGonfiguration? TicketStatesGonfiguration { get; set; }
        public TicketServicesConfiguration? TicketServicesConfiguration { get; set; }
    }
}
