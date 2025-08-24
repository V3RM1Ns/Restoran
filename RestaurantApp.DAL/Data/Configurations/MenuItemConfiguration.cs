using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Core.Enums;
using RestaurantApp.Core.Models;

namespace RestaurantApp.DAL.Data.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(mi => mi.Id);

            builder.Property(mi => mi.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(mi => mi.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); 


            builder.Property(mi => mi.Category)
                   .IsRequired();

            builder.HasData(
                new MenuItem { Id = 1, Name = "Classic Burger", Price = 12.50m, Category = CategoryEnum.MainCourse },
                new MenuItem { Id = 2, Name = "Caesar Salad", Price = 9.00m, Category = CategoryEnum.Salad },
                new MenuItem { Id = 3, Name = "Coca-Cola", Price = 3.00m, Category = CategoryEnum.Beverage },
                new MenuItem { Id = 4, Name = "Fries", Price = 4.50m, Category = CategoryEnum.SideDish },
                new MenuItem { Id = 5, Name = "Chocolate Lava Cake", Price = 7.50m, Category = CategoryEnum.Dessert },
                new MenuItem { Id = 6, Name = "Spaghetti Bolognese", Price = 15.00m, Category = CategoryEnum.MainCourse },
                new MenuItem { Id = 7, Name = "Orange Juice", Price = 4.00m, Category = CategoryEnum.Beverage },
                new MenuItem { Id = 8, Name = "Chicken Wings (6 pcs)", Price = 10.00m, Category = CategoryEnum.Appetizer },
                new MenuItem { Id = 9, Name = "Cheesecake", Price = 6.50m, Category = CategoryEnum.Dessert },
                new MenuItem { Id = 10, Name = "Vegetable Pizza", Price = 14.00m, Category = CategoryEnum.MainCourse }
            );

        }
    }
}
