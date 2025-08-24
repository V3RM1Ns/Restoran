using Microsoft.EntityFrameworkCore;
using RestaurantApp.Core.Models;

namespace RestaurantApp.DAL.Data
{
    public class RestaurantDbContext : DbContext
    {
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Restor;User Id=sa;Password=Hebib123!;Encrypt=False;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
