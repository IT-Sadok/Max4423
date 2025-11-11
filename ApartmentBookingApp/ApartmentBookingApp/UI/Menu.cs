// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
namespace ApartmentBookingApp
{
	internal class Menu(
		HostService hostService,
		ApartmentService apartmentService,
		IOutputWriter outputWriter,
		IInputReader inputReader)
	{
		private int _choice;

		public void GetUserChoice()
		{
			Console.Write("Enter your choice: ");

			while (_choice == 0)
			{
				if (!int.TryParse(Console.ReadLine(), out _choice) || !Enum.IsDefined(typeof(MenuItem), _choice))
				{
					Console.Write("Reenter your choice: ");
					_choice = 0;
				}
			}
		}

		public void ProcessUserChoice()
		{
			switch ((MenuItem)_choice)
			{
				case MenuItem.AddHost:
					hostService.AddHost();
					break;
				case MenuItem.UpdateHost:
					hostService.UpdateHost();
					break;
				case MenuItem.DeleteHost:
					hostService.DeleteHost();
					break;
				case MenuItem.AddApartmentToHost:
					apartmentService.AddApartmentToHost();
					break;
				case MenuItem.ShowHostById:
					if (hostService.GetHostsCount() == 0)
					{
						outputWriter.ShowErrorMessage("Hosts list is empty!");
						break;
					}
					hostService.ShowHosts();
					hostService.ShowHostById(inputReader.ReadIntValue(
						$"Please, enter host id to show details: ",
						v => hostService.GetHostById(v) != null,
						"Incorrect host id. Please reenter."));
					break;
				case MenuItem.ShowAllHosts:
					hostService.ShowHosts();
					break;
				case MenuItem.ShowApartmentsByHostId:
					hostService.ShowHosts();
					if (hostService.GetHostsCount() == 0)
					{
						break;
					}
					apartmentService.ShowApartmentsByHostId(inputReader.ReadIntValue(
						"Please, select host id to show apartments: ",
						v => hostService.GetHostById(v) != null,
						"Incorrect host id. Please reenter."
					));
					break;
				case MenuItem.SaveChanges:
					hostService.SaveChanges();
					outputWriter.ShowSuccessMessage("Changes saved successfully!");

					break;
				case MenuItem.Exit:
					Environment.Exit(0);
					break;
			}
			_choice = 0;
			Console.Write("Press any key to continue: ");
			Console.ReadKey();
		}
	}
}
