using BookingSystem.Domain.Common;
using MediatR;

namespace BookingSystem.Application.Features.Import;

public class ImportDataCommand : IRequest<Result<bool>>
{
    public Stream FileStream { get; }
    public string FileName { get; }

    public ImportDataCommand(Stream fileStream, string fileName)
    {
        FileStream = fileStream;
        FileName = fileName;
    }
}