using RestaurantApp.BL.Dtos.OrderItem;
using RestaurantApp.Core.Models;

namespace RestaurantApp.BL.Profiles
{
    internal class OrderItemProfile
    {
        public static OrderItem OrderItemCreateDtoToOrderItem(OrderItemCreateDto orderItemCreateDto)
        {
            if (orderItemCreateDto == null) return null;

            return new OrderItem
            {
                Count = orderItemCreateDto.Count,
                MenuItemId = orderItemCreateDto.MenuItemId,
            };
        }

        public static OrderItemListDto OrderItemToOrderItemListDto(OrderItem orderItem)
        {
            if (orderItem == null) return null;

            string menuItemName = orderItem.MenuItem?.Name ?? "Unknown Item";
            decimal menuItemPrice = orderItem.MenuItem?.Price ?? 0m;
            decimal menuItemCostValue = orderItem.MenuItem.Price ;
            return new OrderItemListDto
            {
                Id = orderItem.Id,
                Count = orderItem.Count,
                MenuItemId = orderItem.MenuItemId,
                MenuItemName = menuItemName,
                MenuItemPrice = menuItemPrice,
                MenuItemCostValue = menuItemCostValue,
                Subtotal = orderItem.Count * menuItemPrice
            };
        }

        public static List<OrderItemListDto> OrderItemsToOrderItemListDtos(IEnumerable<OrderItem> orderItems)
        {
            if (orderItems == null) return new List<OrderItemListDto>();

            return orderItems.Select(oi => OrderItemToOrderItemListDto(oi)).ToList();
        }
    }
}
