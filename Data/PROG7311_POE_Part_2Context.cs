using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2.Data
{
    public class PROG7311_POE_Part_2Context : DbContext
    {
        public PROG7311_POE_Part_2Context (DbContextOptions<PROG7311_POE_Part_2Context> options)
            : base(options)
        {
        }

        public DbSet<PROG7311_POE_Part_2.Models.Product> Product { get; set; } = default!;
        public DbSet<PROG7311_POE_Part_2.Models.Farmer> Farmer { get; set; } = default!;
    }
}
