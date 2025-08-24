namespace RestaurantApp.PL.Extensions
{
    public static class ConsoleUtility
    {
        public static void WriteColoredLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        public static void WriteError(string message) => WriteColoredLine($"Error: {message}", ConsoleColor.Red);
        public static void WriteSuccess(string message) => WriteColoredLine($"Success: {message}", ConsoleColor.Green);
        public static void WriteWarning(string message) => WriteColoredLine($"Warning: {message}", ConsoleColor.Yellow);
        public static T ReadInput<T>(string prompt, Func<string, (bool isValid, T value)> parser)
        {
            while (true)
            {
                Console.Write($"{prompt} ");
                Console.ForegroundColor = ConsoleColor.Green;
                string input = Console.ReadLine()?.Trim() ?? string.Empty;
                Console.ResetColor();

                var (isValid, value) = parser(input);
                if (isValid)
                {
                    return value;
                }

                WriteError($"Invalid input. Please enter a valid {typeof(T).Name.ToLowerInvariant()}.");
            }
        }
        public static T ReadOptionalInput<T>(string prompt, T fallback, Func<string, (bool isValid, T value)> parser)
        {
            while (true)
            {
                Console.Write($"{prompt} (Leave blank to keep '{fallback}'): ");
                Console.ForegroundColor = ConsoleColor.Green;
                string input = Console.ReadLine()?.Trim() ?? string.Empty;
                Console.ResetColor();

                if (string.IsNullOrWhiteSpace(input))
                {
                    return fallback;
                }

                var (isValid, value) = parser(input);
                if (isValid)
                {
                    return value;
                }

                WriteError($"Invalid input. Please enter a valid {typeof(T).Name.ToLowerInvariant()} or leave blank.");
            }
        }
        public static string ReadStringInput(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} ");
                Console.ForegroundColor = ConsoleColor.Green;
                string input = Console.ReadLine()?.Trim() ?? string.Empty;
                Console.ResetColor();

                if (!string.IsNullOrWhiteSpace(input))
                    return input;
                WriteError("Invalid input. Input cannot be empty, whitespace. Please try again.");
            }
        }

        public static void Pause()
        {
            WriteColoredLine("\nPress any key to continue...", ConsoleColor.DarkGray);
            Console.ReadKey(true);
        }
    }
}
