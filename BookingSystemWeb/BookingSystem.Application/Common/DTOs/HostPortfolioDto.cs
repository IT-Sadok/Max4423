namespace BookingSystem.Application.Common.DTOs;

public record HostPortfolioDto(
    Guid HostId,
    int TotalApartments,
    decimal AveragePrice,
    decimal MinPrice,
    decimal MaxPrice
    );