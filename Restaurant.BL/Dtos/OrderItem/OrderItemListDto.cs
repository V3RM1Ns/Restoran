namespace RestaurantApp.BL.Dtos.OrderItem
{
    public class OrderItemListDto
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public decimal MenuItemPrice { get; set; }
        public int MenuItemQuantity { get; set; }
        public decimal MenuItemCostValue { get; set; }
        public decimal Subtotal { get; set; }
    }
}
