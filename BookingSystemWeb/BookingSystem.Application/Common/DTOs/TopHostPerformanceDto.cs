namespace BookingSystem.Application.Common.DTOs;

public record TopHostPerformanceDto(
    Guid HostId,
    long TotalBookings,
    decimal TotalRevenue
);