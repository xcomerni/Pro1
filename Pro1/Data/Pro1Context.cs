using System;
using System.Collections.Generic;
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

        public DbSet<Pro1.Models.Employee> Employee { get; set; } = default!;
        public DbSet<Pro1.Models.Login> Login { get; set; } = default!;
        public DbSet<Pro1.Models.Ticket> Ticket { get; set; } = default!;
    }
}
