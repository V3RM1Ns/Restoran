namespace RestaurantApp.BL.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class MenuItemNotFoundException : NotFoundException
    {
        public MenuItemNotFoundException() { }
        public MenuItemNotFoundException(string message) : base(message) { }
        public MenuItemNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException() { }
        public OrderNotFoundException(string message) : base(message) { }
        public OrderNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TableNotFoundException : NotFoundException
    {
        public TableNotFoundException() { }
        public TableNotFoundException(string message) : base(message) { }
        public TableNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
