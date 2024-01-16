using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using HaulMaster.Models;

namespace FinalProject.Data
{
    public class FinalProjectContext : DbContext
    {
        public FinalProjectContext (DbContextOptions<FinalProjectContext> options)
            : base(options)
        {
        }

        public DbSet<FinalProject.Models.Product> Product { get; set; } = default!;

        public DbSet<HaulMaster.Models.Driver>? Driver { get; set; }
    }
}
