using RestaurantApp.BL.Dtos.MenuItem;
using RestaurantApp.Core.Enums;

namespace RestaurantApp.BL.Services.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync(MenuItemCreateDto menuItemDto);
        Task RemoveMenuItemAsync(int id);
        Task EditMenuItemAsync(MenuItemUpdateDto menuItemDto);
        Task<List<MenuItemListDto>> GetAllMenuItemsAsync();
        Task<MenuItemListDto> GetMenuItemByIdAsync(int id); 
        Task<List<MenuItemListDto>> GetMenuItemsByCategoryAsync(CategoryEnum category);
        Task<List<MenuItemListDto>> GetMenuItemsByPriceIntervalAsync(decimal minPrice, decimal maxPrice);
        Task<List<MenuItemListDto>> SearchMenuItemsAsync(string searchValue);
    }
}
