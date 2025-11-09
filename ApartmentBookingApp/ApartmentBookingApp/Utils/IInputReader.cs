namespace ApartmentBookingApp;

public interface IInputReader
{
    int ReadIntValue(string message, Func<int, bool> validator, string errorMessage);
    string ReadStringValue(string message, Func<string, bool> validator, string errorMessage);
    decimal ReadDecimalValue(string message, Func<decimal, bool> validator, string errorMessage);
    string ReadOptionalStringValue(string message, Func<string, bool> validator, string errorMessage);
}