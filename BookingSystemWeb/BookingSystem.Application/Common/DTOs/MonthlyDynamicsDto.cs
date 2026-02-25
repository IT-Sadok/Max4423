namespace BookingSystem.Application.Common.DTOs;

public record MonthlyDynamicsDto(
    string BookingMonth,
    long TotalBookings,
    decimal AverageCheck,
    decimal MonthlyRevenue
);