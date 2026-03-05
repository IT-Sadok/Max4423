using FluentValidation;

namespace BookingSystem.Application.Features.Analytics.Queries.GetApartmentOccupancy;

public class GetApartmentOccupancyValidator:AbstractValidator<GetApartmentOccupancyQuery>
{
    public GetApartmentOccupancyValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than zero.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than zero.");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate)
            .WithMessage("StartDate must be before EndDate.");
    }
}