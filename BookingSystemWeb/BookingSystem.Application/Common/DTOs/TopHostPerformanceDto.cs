namespace BookingSystem.Application.Common.DTOs;

public record TopHostPerformanceDto(
    Guid HostId,
    int TotalBookings,
    decimal TotalRevenue
);