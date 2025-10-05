//using CRUD_Demo.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace CRUD_Demo.Data
//{
//    public class ApplicationDbContext : IdentityDbContext
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Employee> Employees { get; set; }
//        public DbSet<Country> Countries { get; set; }
//        public DbSet<State> States { get; set; }
//        public DbSet<City> Cities { get; set; }
//    }
//}





using CRUD_Demo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Demo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.Country)
            //    .WithMany()
            //    .HasForeignKey(e => e.CountryId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.State)
            //    .WithMany()
            //    .HasForeignKey(e => e.StateId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Employee>()
            //    .HasOne(e => e.City)
            //    .WithMany()
            //    .HasForeignKey(e => e.CityId)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Country)
                .WithMany()
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.State)
                .WithMany()
                .HasForeignKey(e => e.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.City)
                .WithMany()
                .HasForeignKey(e => e.CityId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
