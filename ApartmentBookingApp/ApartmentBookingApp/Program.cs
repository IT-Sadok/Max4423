namespace ApartmentBookingApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			BookingSystem bookingSystem = new BookingSystem();
			Menu menu = new Menu();
			while (true)
			{
				menu.ShowMenu();
				menu.GetUserChoice();
				menu.ProcessUserChoice();
			}
		}
	}
}
