namespace BookingSystem.Application.Common.DTOs;

public record ApartmentOccupancyDto(
    Guid ApartmentId,
    string Title,
    long BookingsCount,
    long TotalBookedDays
);