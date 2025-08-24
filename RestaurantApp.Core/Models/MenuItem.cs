using RestaurantApp.Core.Enums;
using RestaurantApp.Core.Models.Common;

namespace RestaurantApp.Core.Models
{
    public class MenuItem : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public CategoryEnum Category { get; set; }
    }
}
