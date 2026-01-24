using FluentValidation;

namespace BookingSystem.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandValidator: AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.ApartmentId)
                .NotEmpty();
        RuleFor(x => x.CheckInDate)
            .LessThan(x => x.CheckOutDate)
            .WithMessage("Check-in date must be before check-out date.");
        RuleFor(x => x.CheckOutDate)
            .GreaterThan(DateTime.UtcNow.Date)
            .WithMessage("Check-out date cannot be in the past.");
    }
}