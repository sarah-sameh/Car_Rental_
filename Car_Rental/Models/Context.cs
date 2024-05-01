using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental.Models
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Car>().HasData(
          new Car
          {
              Id = 1,
              Model = "Camry",
              Make = "Toyota",
              Year = 2020,
              FuelType = "Gasoline",
              IsAvailable = true,
              Image = "camry.jpg",
              Location_Id = 1, // Assuming the location ID is 1
              IsDeleted = false
          },
          new Car
          {
              Id = 2,
              Model = "Accord",
              Make = "Honda",
              Year = 2019,
              FuelType = "Gasoline",
              IsAvailable = true,
              Image = "accord.jpg",
              Location_Id = 2, // Assuming the location ID is 2
              IsDeleted = false
          }
      // Add more cars here as needed
      );



            modelBuilder.Entity<ApplicationUser>().HasData(
         new List<ApplicationUser>
         {
                new ApplicationUser
                {
                    UserName = "user1@example.com",
                    Email = "user1@example.com",
                    Name = "John",
                    PasswordHash = "Doe",
                    PhoneNumber = "+1-555-1234",
                    Address="egypt"
                    // Add other properties you want to set
                },
                new ApplicationUser
                {
                    UserName = "user2@example.com",
                    Email = "user2@example.com",
                    Name = "Jane",
                    PasswordHash = "Doe",
                    PhoneNumber = "+1-555-5678",
                     Address="egypt"
                    // Add other properties you want to set
                },
                new ApplicationUser
                {
                    UserName = "user3@example.com",
                    Email = "user3@example.com",
                    Name = "Michael",
                    PasswordHash = "Smith",
                    PhoneNumber = "+1-555-9012",
                   Address="egypt"
                },
                new ApplicationUser
                {
                    UserName = "user4@example.com",
                    Email = "user4@example.com",
                    Name = "Emily",
                    PasswordHash = "Johnson",
                    PhoneNumber = "+1-555-3456",
                        Address="egypt"
                    // Add other properties you want to set
                },
                new ApplicationUser
                {
                    UserName = "user5@example.com",
                    Email = "user5@example.com",
                    PasswordHash = "William",
                    Name = "Brown",
                    PhoneNumber = "+1-555-7890",
                        Address="egypt"
                    // Add other properties you want to set
                }
         });


            modelBuilder.Entity<Location>().HasData(
    new Location
    {
        Id = 1,
        Name = "Location 1",
        // Add other properties as needed
    },
    new Location
    {
        Id = 2,
        Name = "Location 2",
        // Add other properties as needed
    }
// Add more locations here as needed
);





            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });
        }
    }
}
