﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;

namespace TicketTracker.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task Create(Ticket ticket);

        Task<IEnumerable<Ticket>> GetAll();

        Task<Ticket> GetTicketById(int ticketId);
    }
}
