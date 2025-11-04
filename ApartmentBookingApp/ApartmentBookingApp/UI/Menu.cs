namespace ApartmentBookingApp
{
	internal class Menu
	{
		private HostService _hostService;
		private ApartmentService _apartmentService;
		private readonly IOutputWriter _outputWriter;
		private readonly IInputReader _inputReader;


		private int _choice;
		
		public Menu(HostService hostService, ApartmentService apartmentService, IOutputWriter outputWriter, IInputReader inputReader)
		{
			_hostService = hostService;
			_apartmentService = apartmentService;
			_outputWriter = outputWriter;
			_inputReader = inputReader;
		}
		
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
					_hostService.AddHost();
					break;
				case MenuItem.UpdateHost:
					_hostService.UpdateHost();
					break;
				case MenuItem.DeleteHost:
					_hostService.DeleteHost();
					break;
				case MenuItem.AddApartmentToHost:
					_apartmentService.AddApartmentToHost();
					break;
				case MenuItem.ShowHostById:
					if (_hostService.GetHostsCount() == 0)
					{
						_outputWriter.ShowErrorMessage("Hosts list is empty!");
						break;
					}
					_hostService.ShowHostById(_inputReader.ReadIntValue(
						$"Please, enter host number 1 - {_hostService.GetHostsCount()}: ",
						v => v > 0 && v <= _hostService.GetHostsCount(),
						"Incorrect host number. Please reenter."));
					break;
				case MenuItem.ShowAllHosts:
					_hostService.ShowHosts();
					break;
				case MenuItem.ShowApartmentsByHostId:
					_hostService.ShowHosts();
					if (_hostService.GetHostsCount() == 0)
					{
						break;
					}
					_apartmentService.ShowApartmentsByHostId(_inputReader.ReadIntValue(
						"Please, select host number: ",
						v => v > 0 && v <= _hostService.GetHostsCount(),
						"Incorrect host number. Please reenter."
					));
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
