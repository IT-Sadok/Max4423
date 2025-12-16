using MediatR;

namespace BookingSystem.Application.Features.Auth.Commands.LoginUser;

public class LoginUserCommand: IRequest<string>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}