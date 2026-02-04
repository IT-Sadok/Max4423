using BookingSystem.Application.Common.Interfaces.ImportData;
using BookingSystem.Domain.Common;
using MediatR;

namespace BookingSystem.Application.Features.Import;

public class ImportDataCommandHandler:IRequestHandler<ImportDataCommand, Result<bool>>
{
    public IImportService _importService { get; set; }

    public ImportDataCommandHandler(IImportService importService)
    {
        _importService = importService;
    }
    
    public async Task<Result<bool>> Handle(ImportDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _importService.ImportDataAsync(request.FileStream, request.FileName, cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }
}