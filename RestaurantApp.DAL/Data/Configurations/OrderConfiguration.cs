using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Core.Models;

namespace RestaurantApp.DAL.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Date)
                   .IsRequired()
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(o => o.TotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.HasMany(o => o.OrderItems)
                   .WithOne(oi => oi.Order)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Order { Id = 1, Date = new DateTime(2025, 7, 1, 12, 0, 0), TotalAmount = 20.00m },
                new Order { Id = 2, Date = new DateTime(2025, 7, 3, 14, 30, 0), TotalAmount = 19.00m },
                new Order { Id = 3, Date = new DateTime(2025, 7, 5, 18, 0, 0), TotalAmount = 22.50m },
                new Order { Id = 4, Date = new DateTime(2025, 7, 7, 11, 45, 0), TotalAmount = 24.50m }
            );
        }
    }
}
