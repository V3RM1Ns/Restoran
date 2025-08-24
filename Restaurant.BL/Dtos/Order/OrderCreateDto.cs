using RestaurantApp.BL.Dtos.OrderItem;

namespace RestaurantApp.BL.Dtos.Order
{
    public class OrderCreateDto
    {
        public List<OrderItemCreateDto> OrderItems { get; set; }
    }
}
