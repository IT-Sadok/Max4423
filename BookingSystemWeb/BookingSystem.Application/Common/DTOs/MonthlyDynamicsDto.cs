namespace BookingSystem.Application.Common.DTOs;

public record MonthlyDynamicsDto(
    string BookingMonth,
    int TotalBookings,
    decimal AverageCheck,
    decimal MonthlyRevenue
);