using System.Text.Json;
using BookingSystem.Application.Common.Interfaces.ImportData;
using BookingSystem.Application.Features.Import.DTOs;
using BookingSystem.Domain;
using BookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookingSystem.Infrastructure.ImportData;

public class ImportService : IImportService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ILogger<ImportService> _logger;
    private const int BatchSize = 200;

    public ImportService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher, ILogger<ImportService> logger)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task ImportDataAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
    {
        long currentFileSize = fileStream.Length;

        _logger.LogInformation("Starting import for file '{FileName}'. Size: {Size} bytes", fileName, currentFileSize);
        
        var progress = await _context.ImportProgresses
            .FirstOrDefaultAsync(p => p.FileName == fileName, cancellationToken);

        bool isResume = false;

        if (progress == null)
        {
            _logger.LogInformation("New file detected. Creating tracking record.");
            progress = new ImportProgress
                { FileName = fileName, TotalBytes = currentFileSize, UpdatedAt = DateTime.UtcNow };
            _context.ImportProgresses.Add(progress);
            await _context.SaveChangesAsync(cancellationToken);
        }

        else
        {
            if (progress.TotalBytes != currentFileSize || progress.IsCompleted)
            {
                _logger.LogWarning("File changed or was previously completed. Resetting progress for '{FileName}'", fileName);
                
                progress.LastProcessedExternalId = null;
                progress.ProcessedCount = 0;
                progress.TotalBytes = currentFileSize;
                progress.IsCompleted = false;
                progress.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                isResume = true;
                _logger.LogInformation("Resuming import from LastExternalId: '{LastId}'. Processed so far: {Count}", 
                    progress.LastProcessedExternalId, progress.ProcessedCount);
            }
        }

        string? lastProcessedExternalId = progress?.LastProcessedExternalId;
        bool skipMode = !string.IsNullOrEmpty(lastProcessedExternalId);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var defaultPasswordHash = _passwordHasher.HashPassword(null!, "DefaultUser123!");

        var batchUsers = new List<User>();

        if (fileStream.Position != 0 && fileStream.CanSeek)
        {
            fileStream.Position = 0;
        }

        var userStream =
            JsonSerializer.DeserializeAsyncEnumerable<ImportUserDto>(fileStream, options, cancellationToken);

        await foreach (var userDto in userStream)
        {
            if (userDto == null) continue;

            if (skipMode)
            {
                if (userDto.ExternalId == lastProcessedExternalId)
                {
                    _logger.LogInformation("Found resume point at '{ExternalId}'. Switching to processing mode.", userDto.ExternalId);
                    skipMode = false;
                }

                continue;
            }

            var user = MapUser(userDto, defaultPasswordHash);
            batchUsers.Add(user);

            if (batchUsers.Count >= BatchSize)
            {
                await SaveBatchAsync(batchUsers, progress, cancellationToken);
                batchUsers.Clear();
            }
        }
        
        if (batchUsers.Any())
        {
            await SaveBatchAsync(batchUsers, progress, cancellationToken);
        }

        progress.IsCompleted = true;
        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Import completed successfully for '{FileName}'. Total records: {Total}", fileName, progress.ProcessedCount);
    }

    private User MapUser(ImportUserDto userDto, string passwordHash)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = userDto.Email,
            UserName = userDto.Email,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            PasswordHash = passwordHash,
            ExternalId = userDto.ExternalId,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        foreach (var aptDto in userDto.Apartments)
        {
            user.Apartments.Add(new Apartment(user.Id, aptDto.Title, aptDto.Description, aptDto.Address,
                aptDto.PricePerNight, aptDto.ExternalId));
        }

        return user;
    }

    private async Task SaveBatchAsync(List<User> users, ImportProgress progress, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var externalIds = users.Select(u => u.ExternalId).ToList();

            var existingExternalIds = await _context.Users
                .Where(u => externalIds.Contains(u.ExternalId))
                .Select(u => u.ExternalId)
                .ToListAsync(cancellationToken);

            var newUsers = users
                .Where(u => !existingExternalIds.Contains(u.ExternalId))
                .ToList();
            
            _logger.LogInformation("Saving batch. Total in batch: {BatchCount}. New: {NewCount}. Duplicates skipped: {DupCount}", 
                users.Count, newUsers.Count, existingExternalIds.Count);
            
            if (newUsers.Count > 0)
            {
                await _context.Users.AddRangeAsync(newUsers, cancellationToken);
                progress.ProcessedCount += newUsers.Count;
            }

            if (users.Count > 0)
            {
                progress.LastProcessedExternalId = users.Last().ExternalId!;
                progress.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Failed to save batch. Last ExternalId in batch: {LastId}", users.LastOrDefault()?.ExternalId);
            throw;
        }
    }
}