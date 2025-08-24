using RestaurantApp.Core.Enums;

namespace RestaurantApp.BL.Dtos.MenuItem
{
    public class MenuItemCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
