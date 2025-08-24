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
            

        }
    }
}
