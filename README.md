# 🍽️ RestaurantApp

A clean, modular C# 8.0 (.NET 8) console application for managing restaurant operations including tables, menu items, orders, and daily revenue summaries. This app demonstrates principles of clean architecture, layered design, and Entity Framework Core for data persistence.

---

## 📌 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Entities](#entities)
- [Getting Started](#getting-started)
- [How to Use](#how-to-use)
- [Console Screenshots](#console-screenshots)
- [Project Structure](#project-structure)
- [Planned Improvements](#planned-improvements)
- [Author](#author)

---

## ✅ Features

- 🪑 Table management (add, list, delete, release)
- 🍽️ Menu item management with categories and cost values
- 🧾 Create orders with multiple items and quantities
- 💰 Automatic calculation of order totals and net profit
- 📊 Daily summary: total revenue, total cost, and net profit
- 🎯 Enum-based category selection for menu items
- ⛓️ Proper foreign key relationships between tables and orders
- 📁 Clean Console UI with service layer abstraction

---

## 🛠 Tech Stack

| Layer           | Technology                          |
|----------------|-------------------------------------|
| Language        | C# (.NET 8)                         |
| ORM             | Entity Framework Core               |
| Tools           | Microsoft.EntityFrameworkCore.Tools |
| Architecture    | Clean Architecture (SOLID, SRP)     |
| UI              | Console Application (Text-Based)    |
| Data Storage    | SQLite / Local DB (via EF Core)     |

---

## 🏗️ Architecture

The project follows a **3-layer architecture**:

```

RestaurantApp/
│
├── Core/            # Domain models, DTOs, interfaces, exceptions
├── DAL/             # EF Core configurations, context, repositories
├── Presentation/    # Console UI: order, table, and menu modules
├── Tools/           # Console utility helpers (color output etc.)
└── Program.cs       # App entry point with main navigation

````

Each service is registered via interfaces to support testability and separation of concerns.

---

## 🧱 Key Entities

### 🪑 Table
```csharp
Table
{
    int Id,
    string TableNo,
    bool IsOccupied,
    DateTime? OccupiedAt,
    ICollection<Order> Orders
}
````

### 🍽️ MenuItem

```csharp
MenuItem
{
    int Id,
    string Name,
    decimal Price,
    decimal CostValue,
    CategoryEnum Category
}
```

### 🧾 Order & OrderItem

```csharp
Order
{
    int Id,
    int TableId,
    DateTime CreatedAt,
    DateTime? CompletedAt,
    decimal TotalCost,
    ICollection<OrderItem> OrderItems
}

OrderItem
{
    int Id,
    int OrderId,
    int MenuItemId,
    int Quantity,
    decimal Cost
}
```

---

## 🧪 Getting Started

### ✅ Prerequisites

* .NET 8 SDK
* Visual Studio 2022+ or Rider
* EF Core CLI (`dotnet tool install --global dotnet-ef`)

### ⚙️ Setup

```bash
# 1. Clone the repository
git clone https://github.com/MahirSafar/RestaurantApp.git
cd RestaurantApp

# 2. Restore packages
dotnet restore

# 3. Apply EF Core migrations
dotnet ef database update --project RestaurantApp.DAL

# 4. Run the project
dotnet run --project RestaurantApp.Presentation
```

---

## 🖥️ How to Use

After launching:

```
Welcome to Restaurant App!

1. Manage Tables
2. Manage Menu Items
3. Manage Orders
4. View Daily Summary
0. Exit
```

### 🪑 Manage Tables

* Add a new table with a unique number
* View all tables and their occupancy status
* Mark tables as released after completing orders

### 🍽️ Manage Menu Items

* Add/edit/delete menu items with price, cost, and category
* List menu items with formatted columns

### 🧾 Create Orders

* Select a table and add multiple menu items
* Set quantities and complete the order
* Automatically calculates total cost

### 📊 Daily Summary

* Displays total income, gross cost, and net profit for the day

---

## 📸 Console Screenshots

```text
-----------------------------------------------------------------------------------------------------
Id    Table No        Is Occupied     Occupied At          Total Cost     
-----------------------------------------------------------------------------------------------------
1     Table 101       Yes             14.07.2025 12:01     $39.50
```

```text
--- Daily Summary ---
Total Orders: 4
Gross Revenue: $150.00
Total Cost Value: $95.00
Net Profit: $55.00
```

---

## 📂 Project Structure

```text
RestaurantApp/
│
├── RestaurantApp.Core/
│   ├── Enums/
│   │   └── CategoryEnum.cs
│   └── Models/
│       ├── Common.cs
│       └── BaseEntity.cs
│       ├── MenuItem.cs
│       ├── Order.cs
│       ├── OrderItem.cs
│       └── Table.cs
│
├── RestaurantApp.DAL/
│   ├── Data/
│   │   ├── RestaurantAppDbContext.cs
│   │   └── Configurations/
│   │       ├── MenuItemConfiguration.cs
│   │       ├── OrderConfiguration.cs
│   │       ├── OrderItemConfiguration.cs
│   │       └── TableConfiguration.cs
│   ├── Repositories/
│   │   ├── Concretes/
│   │   │   └── Repository.cs
│   │   └── Interfaces/
│   │       └── IRepository.cs
│   └── Migrations/
│
├── RestaurantApp.PL/
│   ├── UI/
│   │   ├── MenuItemUI.cs
│   │   ├── OrderUI.cs
│   │   ├── TableUI.cs
│   │   └── RestaurantConsoleUI.cs
│   ├── Extensions/
│   │   └── ConsoleUtility/
│   └── Program.cs
│
├── RestaurantApp.BL/
│   ├── Dtos/
│   │   ├── MenuItem/
│   │   │   ├── MenuItemCreateDto.cs
│   │   │   ├── MenuItemListDto.cs
│   │   │   └── MenuItemUpdateDto.cs
│   │   ├── Order/
│   │   │   ├── OrderCreateDto.cs
│   │   │   └── OrderListDto.cs
│   │   ├── OrderItem/
│   │   │   ├── OrderItemCreateDto.cs
│   │   │   └── OrderItemListDto.cs
│   │   └── Table/
│   │       ├── TableCreateDto.cs
│   │       ├── TableListDto.cs
│   │       └── TableUpdateDto.cs
│   ├── Exceptions/
│   │   ├── NotFoundException.cs
│   │   └── ValidationException.cs
│   ├── Profiles/
│   │   ├── MenuItemProfile.cs
│   │   ├── OrderProfile.cs
│   │   ├── OrderItemProfile.cs
│   │   └── TableProfile.cs
│   └── Services/
│       ├── Concretes/
│       │   ├── MenuItemService.cs
│       │   ├── OrderService.cs
│       │   └── TableService.cs
│       └── Interfaces/
│           ├── IMenuItemService.cs
│           ├── IOrderService.cs
│           └── ITableService.cs

```

---

## 🚀 Planned Improvements

* [ ] Add seed data for demo purposes
* [ ] Unit tests using xUnit or NUnit
* [ ] Logging with `ILogger<>`
* [ ] Export daily summary as CSV
* [ ] Add `IsCompleted` flag for orders
* [ ] Track revenue by time range (e.g. weekly, monthly)
* [ ] Add voice commands or speech feedback (bonus idea 🎙️)

---

## 👨‍💻 Author

**Mahir Safar**
🧑‍💻 Full Stack Developer
📍 Azerbaijan
📬 [GitHub](https://github.com/MahirSafar) | [LinkedIn](https://linkedin.com/in/your-profile) *(optional)*

---

## 📄 License

This project is open-source and free to use under the [MIT License](LICENSE).

```
