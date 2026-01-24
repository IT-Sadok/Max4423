using FluentValidation;

namespace BookingSystem.Application.Features.Apartments.Queries.SearchApartments;

public class SearchApartmentsValidator: AbstractValidator<SearchApartmentsQuery>
{
    public SearchApartmentsValidator()
    {
        When(x => x.CheckInDate.HasValue && x.CheckOutDate.HasValue, () =>
        {
            RuleFor(x => x.CheckInDate)
                .LessThan(x => x.CheckOutDate)
                .WithMessage("Check-in date must be before check-out date.");

            RuleFor(x => x.CheckInDate)
                .GreaterThan(DateTime.UtcNow.Date) 
                .WithMessage("Check-in date cannot be in the past.");
        });
    }
}