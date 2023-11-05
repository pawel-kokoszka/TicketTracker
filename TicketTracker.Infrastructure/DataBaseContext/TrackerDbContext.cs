using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTracker.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketTracker.Infrastructure.DataBaseContext
{
    public class TrackerDbContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = DESKTOP - FGUCUTG\\SQLSERVERDEV1; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate=False; Application Intent = ReadWrite; Multi Subnet Failover=False");
        }
    }
}



