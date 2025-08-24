using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Core.Models;

namespace RestaurantApp.DAL.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Count)
                   .IsRequired()
                   .HasDefaultValue(1);

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.MenuItem);

            builder.HasData(
                new OrderItem { Id = 1, OrderId = 1, MenuItemId = 1, Count = 1 }, 
                new OrderItem { Id = 2, OrderId = 1, MenuItemId = 4, Count = 1 }, 
                new OrderItem { Id = 3, OrderId = 1, MenuItemId = 3, Count = 1 }, 

                new OrderItem { Id = 4, OrderId = 2, MenuItemId = 2, Count = 1 }, 
                new OrderItem { Id = 5, OrderId = 2, MenuItemId = 8, Count = 1 }, 

                new OrderItem { Id = 6, OrderId = 3, MenuItemId = 6, Count = 1 }, 
                new OrderItem { Id = 7, OrderId = 3, MenuItemId = 5, Count = 1 }, 

                new OrderItem { Id = 8, OrderId = 4, MenuItemId = 10, Count = 1 },
                new OrderItem { Id = 9, OrderId = 4, MenuItemId = 7, Count = 1 }, 
                new OrderItem { Id = 10, OrderId = 4, MenuItemId = 9, Count = 1 }, 
                new OrderItem { Id = 11, OrderId = 4, MenuItemId = 3, Count = 2 }
            );
        }
    }
}
