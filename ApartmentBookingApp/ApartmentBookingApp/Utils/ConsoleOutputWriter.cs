namespace ApartmentBookingApp;

public class ConsoleOutputWriter: IOutputWriter
{
    public void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Apartment booking menu");
        foreach (MenuItem menuItem in Enum.GetValues(typeof(MenuItem)))
        {
            Console.WriteLine($"{(int)menuItem}. {menuItem}");
        }
    }

    public void ShowHostsList(List<Host> hosts)
    {
        foreach (var host in hosts)
        {
            Console.WriteLine(
                $"# Id: {host.Id} | FullName: {host.FullName} | PhoneNumber: {host.PhoneNumber}");
        }
    }

    public void ShowApartmentsList(Host host)
    {
        for (int i = 0; i < host.Apartments.Count(); i++)
        {
            var apartment = host.Apartments[i];
            Console.WriteLine(
                $"# Id: {apartment.Id} | Title: {apartment.Title} | PricePerNight: {apartment.PricePerNight} | Capacity: {apartment.Capacity}");
        }
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
    
    public void ShowSuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    public void ShowErrorMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}