namespace ApartmentBookingApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var bookingService = new BookingService();
			var menu = new Menu(bookingService);
			while (true)
			{
				menu.ShowMenu();
				menu.GetUserChoice();
				menu.ProcessUserChoice();
			}
		}
	}
}
