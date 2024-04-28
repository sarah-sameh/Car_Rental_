using Microsoft.EntityFrameworkCore;

namespace Car_Rental.Models
{
    public class Context : DbContext
    {
        public DbSet<Car> car { set; get; }
        public DbSet<Location> location { set; get; }
        public DbSet<Maintenance> maintenance { set; get; }
        public DbSet<Rental> rental { set; get; }
        public DbSet<Payment> payments { set; get; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
