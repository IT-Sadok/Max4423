namespace BookingSystem.Application.Features.Analytics.Queries.GetApartmentOccupancy;

public record ApartmentOccupancyDto(
    Guid ApartmentId,
    string Title,
    long BookingsCount,
    long TotalBookedDays
);