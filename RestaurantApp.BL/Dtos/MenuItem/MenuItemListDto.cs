using RestaurantApp.Core.Enums;

namespace RestaurantApp.BL.Dtos.MenuItem
{
    public class MenuItemListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
