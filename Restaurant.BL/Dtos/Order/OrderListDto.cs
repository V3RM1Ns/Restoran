using RestaurantApp.BL.Dtos.OrderItem;

namespace RestaurantApp.BL.Dtos.Order
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public int MenuItemCount { get; set; }
        public List<OrderItemListDto>? OrderItems { get; set; }
    }
}
