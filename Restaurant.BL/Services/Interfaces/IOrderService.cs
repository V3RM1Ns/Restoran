using RestaurantApp.BL.Dtos.Order;

namespace RestaurantApp.BL.Services.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderCreateDto orderDto);
        Task RemoveOrderAsync(int orderId);
        Task<List<OrderListDto>> GetOrdersByDatesIntervalAsync(DateTime startDate, DateTime endDate);
        Task<List<OrderListDto>> GetOrdersByDateAsync(DateTime date);
        Task<List<OrderListDto>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice);
        Task<OrderListDto> GetOrderByNoAsync(int orderId);
        Task<List<OrderListDto>> GetAllOrdersAsync();
    }
}
