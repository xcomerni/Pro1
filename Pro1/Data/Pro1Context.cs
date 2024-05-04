using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pro1.Models;

namespace Pro1.Data
{
    public class Pro1Context : DbContext
    {
        public Pro1Context (DbContextOptions<Pro1Context> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; } = default!;
        public DbSet<Login> Login { get; set; } = default!;
        public DbSet<Ticket> Ticket { get; set; } = default!;
        public DbSet<Callendar> Callendar { get; set; }
    }
}
