namespace BookingSystem.Application.Common.Interfaces.ImportData;

public interface IImportService
{
    Task ImportDataAsync(Stream fileStream, string fileName, CancellationToken cancellationToken);
}