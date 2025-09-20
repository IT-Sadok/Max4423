namespace ApartmentBookingApp
{
	internal class Menu
	{
		int choice = 0;

		readonly List<string> MenuItems = new List<string>
		{
			"Add Host",
			"Add apartment to host",
			"Show all hosts",
			"Show apartments by host id",
			"Book apartment",
			"Exit"
		};

		public void ShowMenu()
		{
			Console.Clear();
			Console.WriteLine("Apartment booking menu");
			for (int i = 0; i < MenuItems.Count; i++)
			{
				Console.WriteLine($"{i + 1}. {MenuItems[i]}");
			}
		}

		public void GetUserChoice()
		{
			Console.Write("Enter your choice: ");

			while (choice == 0)
			{
				if (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > MenuItems.Count)
				{
					Console.Write("Reenter your choice: ");
					choice = 0;
				}
			}
		}

		public void ProcessUserChoice()
		{
			switch (choice)
			{
				case 1:
					BookingSystem.AddHost();
					break;
				case 2:
					BookingSystem.AddApartmentToHost();
					break;
				case 3:
					BookingSystem.ShowHosts();
					break;
				case 4:
					BookingSystem.ShowApartmentByHostId();
					break;
				case 5:
					BookingSystem.BookApartment();
					break;
				case 6:
					Environment.Exit(111);
					break;
			}
			choice = 0;
			Console.Write("Press any key to continue: ");
			Console.ReadKey();
		}
	}
}
