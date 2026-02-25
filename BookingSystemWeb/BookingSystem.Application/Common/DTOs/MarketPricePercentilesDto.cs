namespace BookingSystem.Application.Common.DTOs;

public record MarketPricePercentilesDto(
    double Percentile25,
    double MedianPrice,
    double Percentile75,
    double PremiumThreshold
);