namespace ApartmentBookingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.ShowMenu();

			menu.GetUserChoice();
            menu.ProcessUserChoice();
        }
    }
}
