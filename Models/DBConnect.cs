using Microsoft.EntityFrameworkCore;
using PROG7311_POE_Part_2.Models;

namespace PROG7311_POE_Part_2
{
    public class DBConnect : DbContext
    {
        public DBConnect(DbContextOptions<DBConnect> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Farmer> Farmers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                // Specify the related entity type explicitly  
                .HasOne<Farmer>()
                .WithMany(f => f.Products)
                .HasForeignKey(p => p.FarmerId);
        }
    }
}