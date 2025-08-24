using RestaurantApp.Core.Models.Common;

namespace RestaurantApp.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public int Count { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
