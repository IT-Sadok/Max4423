namespace BookingSystem.Application.Common.DTOs;

public record HostPortfolioDto(
    Guid HostId,
    string HostName,
    long TotalApartments,
    decimal AveragePrice,
    decimal MinPrice,
    decimal MaxPrice
    );