namespace BookingSystem.Application.Common.DTOs;

public record ApartmentOccupancyDto(
    Guid ApartmentId,
    string Title,
    int BookingsCount,
    int TotalBookedDays
);