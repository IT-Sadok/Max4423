namespace ApartmentBookingApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var idGenerator = new IdGeneratorService();
			var bookingService = new BookingService(idGenerator);
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
