namespace RestaurantApp.BL.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class DuplicateMenuItemException : ValidationException
    {
        public DuplicateMenuItemException() { }
        public DuplicateMenuItemException(string message) : base(message) { }
        public DuplicateMenuItemException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidOrderDataException : ValidationException
    {
        public InvalidOrderDataException() { }
        public InvalidOrderDataException(string message) : base(message) { }
        public InvalidOrderDataException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidMenuItemPriceException : ValidationException
    {
        public InvalidMenuItemPriceException() : base("Menu item price is invalid.") { }
        public InvalidMenuItemPriceException(string message) : base(message) { }
        public InvalidMenuItemPriceException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidMenuItemQuantityException : ValidationException
    {
        public InvalidMenuItemQuantityException() : base("Menu item quantity is invalid.") { }
        public InvalidMenuItemQuantityException(string message) : base(message) { }
        public InvalidMenuItemQuantityException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class InvalidMenuItemCostValueException : ValidationException
    {
        public InvalidMenuItemCostValueException() : base("Menu item cost value is invalid.") { }
        public InvalidMenuItemCostValueException(string message) : base(message) { }
        public InvalidMenuItemCostValueException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class DuplicateTableException : ValidationException
    {
        public DuplicateTableException() { }
        public DuplicateTableException(string message) : base(message) { }
        public DuplicateTableException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TableOccupiedException : ValidationException
    {
        public TableOccupiedException() : base("Table is already occupied.") { }
        public TableOccupiedException(string message) : base(message) { }
        public TableOccupiedException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class TableNotOccupiedException : ValidationException
    {
        public TableNotOccupiedException() : base("Table is not currently occupied.") { }
        public TableNotOccupiedException(string message) : base(message) { }
        public TableNotOccupiedException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidTableCapacityException : ValidationException
    {
        public InvalidTableCapacityException() : base("Table capacity is invalid.") { }
        public InvalidTableCapacityException(string message) : base(message) { }
        public InvalidTableCapacityException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class InvalidTableNumberException : ValidationException
    {
        public InvalidTableNumberException() : base("Table number is invalid.") { }
        public InvalidTableNumberException(string message) : base(message) { }
        public InvalidTableNumberException(string message, Exception innerException) : base(message, innerException) { }
    }
}
