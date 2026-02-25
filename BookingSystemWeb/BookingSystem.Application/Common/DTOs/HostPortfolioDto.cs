namespace BookingSystem.Application.Common.DTOs;

public record HostPortfolioDto(
    Guid HostId,
    long TotalApartments,
    decimal AveragePrice,
    decimal MinPrice,
    decimal MaxPrice
    );