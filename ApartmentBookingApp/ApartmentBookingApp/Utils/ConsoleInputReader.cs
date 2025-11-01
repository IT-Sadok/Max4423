namespace ApartmentBookingApp;

public static class ConsoleInputReader
{
    public static int ReadIntValue(string message, Func<int, bool> validator, string errorMessage)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int value) && validator(value))
                return value;
            Console.WriteLine(errorMessage);
        }
    }

    public static decimal ReadDecimalValue(string message, Func<decimal, bool> validator, string errorMessage)
    {
        while (true)
        {
            Console.Write(message);
            if (decimal.TryParse(Console.ReadLine(), out decimal value) && validator(value))
                return value;
            Console.WriteLine(errorMessage);
        }
    }

    public static string ReadStringValue(string message, Func<string, bool> validator, string errorMessage)
    {
        while (true)
        {
            Console.Write(message); 
            string value = Console.ReadLine();
            if (validator(value) && !string.IsNullOrEmpty(value))
                return value;
            Console.WriteLine(errorMessage);
        }
    }
}