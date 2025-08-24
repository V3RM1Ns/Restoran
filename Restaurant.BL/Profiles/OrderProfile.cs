using RestaurantApp.BL.Dtos.Order;
using RestaurantApp.Core.Models;

namespace RestaurantApp.BL.Profiles
{
    public class OrderProfile
    {
        public static Order OrderCreateDtoToOrder(OrderCreateDto orderCreateDto)
        {
            if (orderCreateDto == null) return null;

            return new Order
            {
                OrderItems = orderCreateDto.OrderItems?.Select(oiDto => new OrderItem
                {
                    MenuItemId = oiDto.MenuItemId,
                    Count = oiDto.Count,
                }).ToList() ?? []
            };
        }

        public static OrderListDto OrderToOrderListDto(Order order)
        {
            if (order == null) return null;

            var orderItemsListDtos = order.OrderItems?
                .Select(oi => OrderItemProfile.OrderItemToOrderItemListDto(oi))
                .ToList() ?? [];

            int menuItemCount = order.OrderItems?.Sum(oi => oi.Count) ?? 0;

            return new OrderListDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                Date = order.Date,
                MenuItemCount = menuItemCount,
                OrderItems = orderItemsListDtos
            };
        }

        public static List<OrderListDto> OrdersToOrderListDtos(IEnumerable<Order> orders)
        {
            if (orders == null) return [];
            return orders.Select(o => OrderToOrderListDto(o)).ToList();
        }
    }
}
