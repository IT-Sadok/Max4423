using ApartmentBookingApp.Repository;

namespace ApartmentBookingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var outputWriter = new ConsoleOutputWriter();
            var inputReader = new ConsoleInputReader();
            var hostRepository = HostRepository.Create();
            var idGenerator = new IdGeneratorService();
            idGenerator.Initialize(hostRepository.GetMaxHostId(), hostRepository.GetMaxApartmentId());
            var hostService = new HostService(hostRepository, idGenerator, outputWriter, inputReader);
            var apartmentService = new ApartmentService(hostService, idGenerator, outputWriter, inputReader);
            var menu = new Menu(hostService, apartmentService, outputWriter, inputReader);
            while (true)
            {
                outputWriter.ShowMenu();
                menu.GetUserChoice();
                menu.ProcessUserChoice();
            }
        }
    }
}