﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketTracker.Application.Tickets
{
    public class UserDto
    {
        public string? UserId { get; set; }

        public List<int>? TeamsList { get; set; }
    }
}
