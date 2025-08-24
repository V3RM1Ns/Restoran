using RestaurantApp.BL.Dtos.MenuItem;
using RestaurantApp.Core.Models;

namespace RestaurantApp.BL.Profiles
{
    public class MenuItemProfile
    {
        public static MenuItem MenuItemCreateDtoToMenuItem(MenuItemCreateDto menuItemCreateDto)
        {
            return new MenuItem
            {
                Name = menuItemCreateDto.Name,
                Price = menuItemCreateDto.Price,
                Category = menuItemCreateDto.Category
            };
        }
        
        public static void UpdateMenuItemFromDto(MenuItem menuItem, MenuItemUpdateDto dto)
        {
            if (menuItem == null || dto == null) return;

            menuItem.Name = dto.Name;
            menuItem.Price = dto.Price;
            menuItem.Category = dto.Category;
        }

        public static MenuItemListDto MenuItemToMenuItemListDto(MenuItem menuItem)
        {
            return new MenuItemListDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Price = menuItem.Price,
                Category = menuItem.Category
            };
        }

        public static List<MenuItemListDto> MenuItemsToMenuItemListDtos(List<MenuItem> menuItems) => menuItems.Select(MenuItemToMenuItemListDto).ToList();
    }
}
