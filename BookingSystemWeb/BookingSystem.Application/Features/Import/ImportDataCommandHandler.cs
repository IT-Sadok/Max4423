using BookingSystem.Application.Common.Interfaces.ImportData;
using BookingSystem.Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Application.Features.Import;

public class ImportDataCommandHandler:IRequestHandler<ImportDataCommand, Result<bool>>
{
    private readonly IImportService _importService;
    private readonly ILogger<ImportDataCommandHandler> _logger;


    public ImportDataCommandHandler(IImportService importService, ILogger<ImportDataCommandHandler> logger)
    {
        _importService = importService;
        _logger = logger;
    }
    
    public async Task<Result<bool>> Handle(ImportDataCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _importService.ImportDataAsync(request.FileStream, request.FileName, request.FileSize, cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while importing file {request.FileName}");
            return Result<bool>.Failure("Import failed");
        }
    }
}