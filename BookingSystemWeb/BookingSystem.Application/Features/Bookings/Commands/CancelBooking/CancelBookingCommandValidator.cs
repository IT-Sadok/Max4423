using FluentValidation;

namespace BookingSystem.Application.Features.Bookings.Commands.CancelBooking;

public class CancelBookingCommandValidator: AbstractValidator<CancelBookingCommand>
{
    public CancelBookingCommandValidator()
    {
        RuleFor(x => x.BookingId)
            .NotEmpty()
            .WithMessage("BookingId is required.");
    }
}