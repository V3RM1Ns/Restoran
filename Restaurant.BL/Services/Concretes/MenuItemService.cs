using RestaurantApp.BL.Dtos.MenuItem;
using RestaurantApp.BL.Profiles; 
using RestaurantApp.BL.Services.Interfaces;
using RestaurantApp.Core.Enums;
using RestaurantApp.BL.Exceptions;
using RestaurantApp.Core.Models;
using RestaurantApp.DAL.Repositories.Concretes;
using RestaurantApp.DAL.Repositories.Interfaces;

namespace RestaurantApp.BL.Services.Concretes
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _menuItemRepository;
        public MenuItemService()
        {
            _menuItemRepository = new Repository<MenuItem>(); 
        }
        private List<MenuItemListDto> MapMenuItemsToDtoList(List<MenuItem> menuItems) => MenuItemProfile.MenuItemsToMenuItemListDtos(menuItems);
        
        public async Task AddMenuItemAsync(MenuItemCreateDto menuItemDto)
        {
            if (menuItemDto is null)
                throw new ArgumentNullException(nameof(menuItemDto), "Menu item data cannot be null.");

            var existingItems = await _menuItemRepository.GetAllAsync();
            if (existingItems.Any(m => m.Name.ToLower() == menuItemDto.Name.ToLower()))
                throw new DuplicateMenuItemException($"A menu item with the name '{menuItemDto.Name}' already exists.");

            if (menuItemDto.Price <= 0)
                throw new InvalidMenuItemPriceException("Menu item price must be greater than zero.");

            var menuItem = MenuItemProfile.MenuItemCreateDtoToMenuItem(menuItemDto);
            await _menuItemRepository.CreateAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();
        }

        public async Task EditMenuItemAsync(MenuItemUpdateDto menuItemDto)
        {
            if (menuItemDto is null)
                throw new ArgumentNullException(nameof(menuItemDto), "Menu item update data cannot be null.");

            var menuItem = await _menuItemRepository.GetByIdAsync(menuItemDto.Id) ?? 
                throw new MenuItemNotFoundException($"Menu item with ID {menuItemDto.Id} not found.");

            var allItems = await _menuItemRepository.GetAllAsync();
            if (allItems.Any(t => t.Name.ToLower() == menuItemDto.Name.ToLower() && t.Id != menuItemDto.Id))
                throw new DuplicateMenuItemException($"A menu item with the name '{menuItemDto.Name}' already exists.");

            if (menuItemDto.Price <= 0)
                throw new InvalidMenuItemPriceException("Menu item price must be greater than zero.");

            MenuItemProfile.UpdateMenuItemFromDto(menuItem, menuItemDto);

            await _menuItemRepository.UpdateAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();
        }

        public async Task<List<MenuItemListDto>> GetAllMenuItemsAsync()
        {
            var menuItems = await _menuItemRepository.GetAllAsync();
            if (!menuItems.Any())
                throw new MenuItemNotFoundException("Not found any menu item.");
            return MapMenuItemsToDtoList(menuItems);
        }

        public async Task<List<MenuItemListDto>> GetMenuItemsByCategoryAsync(CategoryEnum category)
        {
            var allItems = await _menuItemRepository.GetAllAsync();
            var filteredMenuItems = allItems.Where(m => m.Category == category).ToList();
            if (!filteredMenuItems.Any())
                throw new MenuItemNotFoundException("Not found any menu item in this category.");
            return MapMenuItemsToDtoList(filteredMenuItems);
        }

        public async Task<List<MenuItemListDto>> GetMenuItemsByPriceIntervalAsync(decimal minPrice, decimal maxPrice)
        {
            var allItems = await _menuItemRepository.GetAllAsync();
            var filteredMenuItems = allItems.Where(m => m.Price >= minPrice && m.Price <= maxPrice).ToList();
            if (!filteredMenuItems.Any())
                throw new MenuItemNotFoundException("Not found any menu item in this price interval.");
            return MapMenuItemsToDtoList(filteredMenuItems);
        }

        public async Task RemoveMenuItemAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id) ?? 
                throw new MenuItemNotFoundException($"Menu item with ID {id} not found.");
            await _menuItemRepository.DeleteAsync(id);
            await _menuItemRepository.SaveChangesAsync();
        }

        public async Task<List<MenuItemListDto>> SearchMenuItemsAsync(string searchValue)
        {
            if (string.IsNullOrWhiteSpace(searchValue))
                return await GetAllMenuItemsAsync();

            string lowerSearchValue = searchValue.ToLower();
            var allItems = await _menuItemRepository.GetAllAsync();
            var filteredMenuItems = allItems.Where(m => 
                m.Name.ToLower().Contains(lowerSearchValue) ||
                m.Category.ToString().ToLower().Contains(lowerSearchValue)).ToList();

            return MapMenuItemsToDtoList(filteredMenuItems);
        }

        public async Task<MenuItemListDto> GetMenuItemByIdAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id) ?? 
                throw new MenuItemNotFoundException($"Menu item with ID {id} not found.");
            return MenuItemProfile.MenuItemToMenuItemListDto(menuItem);
        }
    }
}
