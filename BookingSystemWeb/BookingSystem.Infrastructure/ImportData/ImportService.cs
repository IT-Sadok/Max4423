using System.Text.Json;
using BookingSystem.Application.Common.Interfaces.ImportData;
using BookingSystem.Application.Features.Import.DTOs;
using BookingSystem.Domain;
using BookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.ImportData;

public class ImportService : IImportService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;
    private const int BatchSize = 5000;

    public ImportService(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task ImportDataAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
    {
        var progress = await _context.ImportProgresses
            .FirstOrDefaultAsync(p => p.FileName == fileName, cancellationToken);

        string? lastProcessedExternalId = progress?.LastProcessedExternalId;
        bool skipMode = !string.IsNullOrEmpty(lastProcessedExternalId);

        if (progress == null)
        {
            progress = new ImportProgress { FileName = fileName, UpdatedAt = DateTime.UtcNow };
            _context.ImportProgresses.Add(progress);
            await _context.SaveChangesAsync(cancellationToken);
        }

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var defaultPasswordHash = _passwordHasher.HashPassword(null!, "DefaultUser123!");

        var batchUsers = new List<User>();

        var userStream =
            JsonSerializer.DeserializeAsyncEnumerable<ImportUserDto>(fileStream, options, cancellationToken);

        await foreach (var userDto in userStream)
        {
            if (userDto == null) continue;

            if (skipMode)
            {
                if (userDto.ExternalId == lastProcessedExternalId)
                {
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
            await _context.Users.AddRangeAsync(users, cancellationToken);
            if (users.Count > 0)
            {
                progress.LastProcessedExternalId = users.Last().ExternalId!;
                progress.ProcessedCount += users.Count;
                progress.UpdatedAt = DateTime.UtcNow;
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}