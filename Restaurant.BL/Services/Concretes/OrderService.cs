using Microsoft.EntityFrameworkCore; 
using RestaurantApp.BL.Dtos.Order;
using RestaurantApp.BL.Profiles; 
using RestaurantApp.BL.Services.Interfaces;
using RestaurantApp.BL.Exceptions;
using RestaurantApp.Core.Models;
using RestaurantApp.DAL.Repositories.Concretes;
using RestaurantApp.DAL.Repositories.Interfaces;

namespace RestaurantApp.BL.Services.Concretes
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IMenuItemService _menuItemService;
        
        public OrderService(IMenuItemService menuItemService)
        {
            _orderRepository = new Repository<Order>();
            _menuItemService = menuItemService;
        }
        
        private OrderListDto MapOrderToDto(Order order) => OrderProfile.OrderToOrderListDto(order);
        private List<OrderListDto> MapOrdersToDtoList(IEnumerable<Order> orders) => OrderProfile.OrdersToOrderListDtos(orders);
        
        public async Task AddOrderAsync(OrderCreateDto orderDto)
        {
            if (orderDto is null)
                throw new ArgumentNullException(nameof(orderDto), "Order data cannot be null.");
            if (!orderDto.OrderItems.Any())
                throw new InvalidOrderDataException("An order must contain at least one item.");

            Order order = new Order
            {
                Date = DateTime.Now, 
                OrderItems = []
            };

            decimal calculatedTotalAmount = 0;

            foreach (var itemDto in orderDto.OrderItems)
            {
                var menuItem = await _menuItemService.GetMenuItemByIdAsync(itemDto.MenuItemId) ??
                    throw new MenuItemNotFoundException($"Menu item with ID {itemDto.MenuItemId} not found for order item.");
                if (itemDto.Count <= 0)
                    throw new InvalidOrderDataException($"Count for menu item {menuItem.Name} must be positive.");

                order.OrderItems.Add(new OrderItem
                {
                    MenuItemId = itemDto.MenuItemId,
                    Count = itemDto.Count,
                });
                calculatedTotalAmount += menuItem.Price * itemDto.Count;
            }

            order.TotalAmount = calculatedTotalAmount;
            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveChangesAsync(); 
        }
        
        public async Task RemoveOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId) ?? 
                throw new OrderNotFoundException($"Order with ID {orderId} not found.");

            await _orderRepository.DeleteAsync(orderId);
            await _orderRepository.SaveChangesAsync();
        }
        
        public async Task<List<OrderListDto>> GetOrdersByDatesIntervalAsync(DateTime startDate, DateTime endDate)
        {
            var allOrders = await _orderRepository.GetAllAsync();
            var filteredOrders = allOrders.Where(o => o.Date.Date >= startDate.Date && o.Date.Date <= endDate.Date).ToList();
            
            if (!filteredOrders.Any())
                throw new OrderNotFoundException($"Order not found in this interval.");

            return MapOrdersToDtoList(filteredOrders);
        }
        
        public async Task<List<OrderListDto>> GetOrdersByDateAsync(DateTime date)
        {
            var allOrders = await _orderRepository.GetAllAsync();
            var filteredOrders = allOrders.Where(o => o.Date.Date == date.Date).ToList();
            
            if (!filteredOrders.Any())
                throw new OrderNotFoundException($"Order not found.");

            return MapOrdersToDtoList(filteredOrders);
        }
        
        public async Task<List<OrderListDto>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice)
        {
            var allOrders = await _orderRepository.GetAllAsync();
            var filteredOrders = allOrders.Where(o => o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice).ToList();
            
            if (!filteredOrders.Any())
                throw new OrderNotFoundException($"Order not found in this interval.");

            return MapOrdersToDtoList(filteredOrders);
        }
        
        public async Task<List<OrderListDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            if (!orders.Any())
                throw new OrderNotFoundException($"Order not found.");
            return MapOrdersToDtoList(orders);
        }
        
        public async Task<OrderListDto> GetOrderByNoAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId) ?? 
                throw new OrderNotFoundException($"Order with ID {orderId} not found.");
            return MapOrderToDto(order);
        }
    }
}
