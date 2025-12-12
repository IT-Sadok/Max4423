using FluentValidation;
namespace BookingSystem.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Email is not valid.");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name cannot be empty.")
            .MaximumLength(50).WithMessage("First name cannot be more than 50 characters long.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name cannot be empty.")
            .MaximumLength(50).WithMessage("Last name cannot be more than 50 characters long.");
    }
}