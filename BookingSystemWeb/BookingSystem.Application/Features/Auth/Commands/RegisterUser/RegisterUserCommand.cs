using MediatR;
namespace BookingSystem.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommand: IRequest<Guid>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}