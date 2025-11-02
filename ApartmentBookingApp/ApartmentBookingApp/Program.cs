using ApartmentBookingApp.Repository;

namespace ApartmentBookingApp
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var hostRepository = new HostRepository();
			var idGenerator = new IdGeneratorService();
			var hostService = new HostService(hostRepository, idGenerator);
			var apartmentService = new ApartmentService(hostService, idGenerator);
			var menu = new Menu(hostService, apartmentService);
			while (true)
			{
				menu.ShowMenu();
				menu.GetUserChoice();
				menu.ProcessUserChoice();
			}
		}
	}
}
