using BookingSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Application.Common.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<Apartment> Apartments { get; }
    DbSet<Booking> Bookings { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}