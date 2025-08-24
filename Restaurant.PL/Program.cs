using RestaurantApp.BL.Dtos.MenuItem;
using RestaurantApp.BL.Dtos.Order;
using RestaurantApp.BL.Dtos.OrderItem;
using RestaurantApp.BL.Services.Concretes;
using RestaurantApp.BL.Services.Interfaces;
using RestaurantApp.Core.Enums;

namespace RestaurantApp.PL
{
    internal class Program
    {
        private static IMenuItemService? _menuItemService;
        private static IOrderService? _orderService;

        static async Task Main(string[] _)
        {
            _menuItemService = new MenuItemService();
            _orderService = new OrderService(_menuItemService);

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== RESTORAN SİFARİŞ SİSTEMİNE XOŞ GELMİŞSİNİZ ===\n");

            while (true)
            {
                try
                {
                    await ShowMainMenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Xəta baş verdi: {ex.Message}");
                    Console.WriteLine("Davam etmək üçün bir düyməyə basın...");
                    Console.ReadKey();
                }
            }
        }

        private static async Task ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ƏSAS MENYU ===");
            Console.WriteLine("1. Menu üzərində əməliyyat aparmaq");
            Console.WriteLine("2. Sifarişlər üzərində əməliyyat aparmaq");
            Console.WriteLine("0. Sistemdən çıxmaq");
            Console.Write("\nSeçiminizi edin: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ShowMenuItemOperations();
                    break;
                case "2":
                    await ShowOrderOperations();
                    break;
                case "0":
                    Console.WriteLine("Sistemdən çıxılır...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Yanlış seçim! Davam etmək üçün bir düyməyə basın...");
                    Console.ReadKey();
                    break;
            }
        }

        private static async Task ShowMenuItemOperations()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MENU İTEMLƏRİ ÜZƏRİNDƏ ƏMƏLİYYATLAR ===");
                Console.WriteLine("1. Yeni item əlavə et");
                Console.WriteLine("2. Item üzərində düzəliş et");
                Console.WriteLine("3. Item sil");
                Console.WriteLine("4. Bütün itemları göstər");
                Console.WriteLine("5. Kateqoriyasına görə menu itemları göstər");
                Console.WriteLine("6. Qiymət aralığına görə menu itemlar göstər");
                Console.WriteLine("7. Menu itemlar arasında ada görə axtarış et");
                Console.WriteLine("0. Əvvəlki menyuya qayıt");
                Console.Write("\nSeçiminizi edin: ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await AddMenuItem();
                            break;
                        case "2":
                            await EditMenuItem();
                            break;
                        case "3":
                            await RemoveMenuItem();
                            break;
                        case "4":
                            await ShowAllMenuItems();
                            break;
                        case "5":
                            await ShowMenuItemsByCategory();
                            break;
                        case "6":
                            await ShowMenuItemsByPriceRange();
                            break;
                        case "7":
                            await SearchMenuItems();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Yanlış seçim!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Xəta: {ex.Message}");
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nDavam etmək üçün bir düyməyə basın...");
                    Console.ReadKey();
                }
            }
        }

        private static async Task ShowOrderOperations()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SİFARİŞLƏR ÜZƏRİNDƏ ƏMƏLİYYATLAR ===");
                Console.WriteLine("1. Yeni sifariş əlavə etmək");
                Console.WriteLine("2. Sifarişin ləğvi");
                Console.WriteLine("3. Bütün sifarişlərin ekrana çıxarılması");
                Console.WriteLine("4. Verilən tarix aralığına görə sifarişlərin göstərilməsi");
                Console.WriteLine("5. Verilən məbləğ aralığına görə sifarişlərin göstərilməsi");
                Console.WriteLine("6. Verilmiş bir tarixdə olan sifarişlərin göstərilməsi");
                Console.WriteLine("7. Verilmiş nömrəyə əsasən həmin nömrəli sifarişin məlumatlarının göstərilməsi");
                Console.WriteLine("0. Əvvəlki menyuya qayıt");
                Console.Write("\nSeçiminizi edin: ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await AddOrder();
                            break;
                        case "2":
                            await RemoveOrder();
                            break;
                        case "3":
                            await ShowAllOrders();
                            break;
                        case "4":
                            await ShowOrdersByDateRange();
                            break;
                        case "5":
                            await ShowOrdersByPriceRange();
                            break;
                        case "6":
                            await ShowOrdersByDate();
                            break;
                        case "7":
                            await ShowOrderById();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Yanlış seçim!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Xəta: {ex.Message}");
                }

                if (choice != "0")
                {
                    Console.WriteLine("\nDavam etmək üçün bir düyməyə basın...");
                    Console.ReadKey();
                }
            }
        }

        // MenuItem Operations
        private static async Task AddMenuItem()
        {
            Console.Clear();
            Console.WriteLine("=== YENİ MENU İTEMİ ƏLAVƏ ET ===");

            Console.Write("Adı: ");
            var name = Console.ReadLine() ?? "";

            Console.Write("Qiyməti: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Yanlış qiymət formatı!");
                return;
            }

            Console.WriteLine("Kateqoriyalar:");
            var categories = Enum.GetValues<CategoryEnum>();
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i]}");
            }

            Console.Write("Kateqoriya seçin (nömrə): ");
            if (!int.TryParse(Console.ReadLine(), out int categoryChoice) || 
                categoryChoice < 1 || categoryChoice > categories.Length)
            {
                Console.WriteLine("Yanlış kateqoriya seçimi!");
                return;
            }

            var dto = new MenuItemCreateDto
            {
                Name = name,
                Price = price,
                Category = categories[categoryChoice - 1]
            };

            await _menuItemService!.AddMenuItemAsync(dto);
            Console.WriteLine("Menu item uğurla əlavə edildi!");
        }

        private static async Task EditMenuItem()
        {
            Console.Clear();
            Console.WriteLine("=== MENU İTEMİNİ DÜZƏLİŞ ET ===");

            Console.Write("Düzəliş ediləcək menu item ID-si: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Yanlış ID formatı!");
                return;
            }

            var existingItem = await _menuItemService!.GetMenuItemByIdAsync(id);
            Console.WriteLine($"Mövcud məlumatlar - Ad: {existingItem.Name}, Qiymət: {existingItem.Price}, Kateqoriya: {existingItem.Category}");

            Console.Write("Yeni ad: ");
            var name = Console.ReadLine() ?? "";

            Console.Write("Yeni qiymət: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Yanlış qiymət formatı!");
                return;
            }

            Console.WriteLine("Kateqoriyalar:");
            var categories = Enum.GetValues<CategoryEnum>();
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i]}");
            }

            Console.Write("Yeni kateqoriya seçin (nömrə): ");
            if (!int.TryParse(Console.ReadLine(), out int categoryChoice) || 
                categoryChoice < 1 || categoryChoice > categories.Length)
            {
                Console.WriteLine("Yanlış kateqoriya seçimi!");
                return;
            }

            var dto = new MenuItemUpdateDto
            {
                Id = id,
                Name = name,
                Price = price,
                Category = categories[categoryChoice - 1]
            };

            await _menuItemService!.EditMenuItemAsync(dto);
            Console.WriteLine("Menu item uğurla yeniləndi!");
        }

        private static async Task RemoveMenuItem()
        {
            Console.Clear();
            Console.WriteLine("=== MENU İTEMİNİ SİL ===");

            Console.Write("Silinəcək menu item ID-si: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Yanlış ID formatı!");
                return;
            }

            await _menuItemService!.RemoveMenuItemAsync(id);
            Console.WriteLine("Menu item uğurla silindi!");
        }

        private static async Task ShowAllMenuItems()
        {
            Console.Clear();
            Console.WriteLine("=== BÜTÜN MENU İTEMLƏRİ ===");

            var items = await _menuItemService!.GetAllMenuItemsAsync();
            DisplayMenuItems(items);
        }

        private static async Task ShowMenuItemsByCategory()
        {
            Console.Clear();
            Console.WriteLine("=== KATEQORİYAYA GÖRƏ MENU İTEMLƏRİ ===");

            Console.WriteLine("Kateqoriyalar:");
            var categories = Enum.GetValues<CategoryEnum>();
            for (int i = 0; i < categories.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i]}");
            }

            Console.Write("Kateqoriya seçin (nömrə): ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || 
                choice < 1 || choice > categories.Length)
            {
                Console.WriteLine("Yanlış kateqoriya seçimi!");
                return;
            }

            var selectedCategory = categories[choice - 1];
            var items = await _menuItemService!.GetMenuItemsByCategoryAsync(selectedCategory);
            DisplayMenuItems(items);
        }

        private static async Task ShowMenuItemsByPriceRange()
        {
            Console.Clear();
            Console.WriteLine("=== QİYMƏT ARALIĞINA GÖRƏ MENU İTEMLƏRİ ===");

            Console.Write("Minimum qiymət: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
            {
                Console.WriteLine("Yanlış qiymət formatı!");
                return;
            }

            Console.Write("Maksimum qiymət: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
            {
                Console.WriteLine("Yanlış qiymət formatı!");
                return;
            }

            var items = await _menuItemService!.GetMenuItemsByPriceIntervalAsync(minPrice, maxPrice);
            DisplayMenuItems(items);
        }

        private static async Task SearchMenuItems()
        {
            Console.Clear();
            Console.WriteLine("=== MENU İTEMLƏRİNDƏ AXTARIŞ ===");

            Console.Write("Axtarış mətni: ");
            var searchText = Console.ReadLine() ?? "";

            var items = await _menuItemService!.SearchMenuItemsAsync(searchText);
            DisplayMenuItems(items);
        }

        // Order Operations
        private static async Task AddOrder()
        {
            Console.Clear();
            Console.WriteLine("=== YENİ SİFARİŞ ƏLAVƏ ET ===");

            var orderItems = new List<OrderItemCreateDto>();

            while (true)
            {
                Console.WriteLine("\nMövcud menu itemlər:");
                var menuItems = await _menuItemService!.GetAllMenuItemsAsync();
                DisplayMenuItems(menuItems);

                Console.Write("\nMenu item ID-si (0 - bitir): ");
                if (!int.TryParse(Console.ReadLine(), out int menuItemId))
                {
                    Console.WriteLine("Yanlış ID formatı!");
                    continue;
                }

                if (menuItemId == 0) break;

                Console.Write("Say: ");
                if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
                {
                    Console.WriteLine("Yanlış say formatı!");
                    continue;
                }

                orderItems.Add(new OrderItemCreateDto
                {
                    MenuItemId = menuItemId,
                    Count = count
                });

                Console.WriteLine("Item əlavə edildi!");
            }

            if (!orderItems.Any())
            {
                Console.WriteLine("Sifariş üçün heç bir item əlavə edilmədi!");
                return;
            }

            var orderDto = new OrderCreateDto
            {
                OrderItems = orderItems
            };

            await _orderService!.AddOrderAsync(orderDto);
            Console.WriteLine("Sifariş uğurla əlavə edildi!");
        }

        private static async Task RemoveOrder()
        {
            Console.Clear();
            Console.WriteLine("=== SİFARİŞİ LƏĞV ET ===");

            Console.Write("Ləğv ediləcək sifariş ID-si: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Yanlış ID formatı!");
                return;
            }

            await _orderService!.RemoveOrderAsync(orderId);
            Console.WriteLine("Sifariş uğurla ləğv edildi!");
        }

        private static async Task ShowAllOrders()
        {
            Console.Clear();
            Console.WriteLine("=== BÜTÜN SİFARİŞLƏR ===");

            var orders = await _orderService!.GetAllOrdersAsync();
            DisplayOrders(orders);
        }

        private static async Task ShowOrdersByDateRange()
        {
            Console.Clear();
            Console.WriteLine("=== TARİX ARALIĞINA GÖRƏ SİFARİŞLƏR ===");

            Console.Write("Başlanğıc tarixi (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Yanlış tarix formatı!");
                return;
            }

            Console.Write("Son tarix (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Yanlış tarix formatı!");
                return;
            }

            var orders = await _orderService!.GetOrdersByDatesIntervalAsync(startDate, endDate);
            DisplayOrders(orders);
        }

        private static async Task ShowOrdersByPriceRange()
        {
            Console.Clear();
            Console.WriteLine("=== MƏBLƏĞ ARALIĞINA GÖRƏ SİFARİŞLƏR ===");

            Console.Write("Minimum məbləğ: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
            {
                Console.WriteLine("Yanlış məbləğ formatı!");
                return;
            }

            Console.Write("Maksimum məbləğ: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
            {
                Console.WriteLine("Yanlış məbləğ formatı!");
                return;
            }

            var orders = await _orderService!.GetOrdersByPriceIntervalAsync(minPrice, maxPrice);
            DisplayOrders(orders);
        }

        private static async Task ShowOrdersByDate()
        {
            Console.Clear();
            Console.WriteLine("=== TARİXƏ GÖRƏ SİFARİŞLƏR ===");

            Console.Write("Tarix (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Yanlış tarix formatı!");
                return;
            }

            var orders = await _orderService!.GetOrdersByDateAsync(date);
            DisplayOrders(orders);
        }

        private static async Task ShowOrderById()
        {
            Console.Clear();
            Console.WriteLine("=== SİFARİŞİN ƏTRAFLI MƏLUMATLARI ===");

            Console.Write("Sifariş ID-si: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId))
            {
                Console.WriteLine("Yanlış ID formatı!");
                return;
            }

            var order = await _orderService!.GetOrderByNoAsync(orderId);
            DisplayOrderDetails(order);
        }

        // Display Methods
        private static void DisplayMenuItems(List<MenuItemListDto> items)
        {
            Console.WriteLine("\n" + new string('-', 80));
            Console.WriteLine($"{"ID",-5} {"Ad",-20} {"Kateqoriya",-15} {"Qiymət",-10}");
            Console.WriteLine(new string('-', 80));

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id,-5} {item.Name,-20} {item.Category,-15} {item.Price,-10:C}");
            }
            Console.WriteLine(new string('-', 80));
        }

        private static void DisplayOrders(List<OrderListDto> orders)
        {
            Console.WriteLine("\n" + new string('-', 80));
            Console.WriteLine($"{"ID",-5} {"Məbləğ",-15}  {"Tarix",-20}");
            Console.WriteLine(new string('-', 80));

            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Id,-5} {order.TotalAmount,-15:C} {order.Date,-20:dd.MM.yyyy HH:mm}");
            }
            Console.WriteLine(new string('-', 80));
        }

        private static void DisplayOrderDetails(OrderListDto order)
        {
            Console.WriteLine($"\nSifariş ID: {order.Id}");
            Console.WriteLine($"Məbləğ: {order.TotalAmount:C}");
            Console.WriteLine($"Tarix: {order.Date:dd.MM.yyyy HH:mm}");
            
            if (order.OrderItems?.Any() == true)
            {
                Console.WriteLine("\nSifariş itemləri:");
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"{"ID",-5} {"Ad",-20} {"Say",-10}");
                Console.WriteLine(new string('-', 60));
                
                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine($"{item.MenuItemId,-5} {item.MenuItemName,-20} {item.Count,-10}");
                }
                Console.WriteLine(new string('-', 60));
            }
        }
    }
}
