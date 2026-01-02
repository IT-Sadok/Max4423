using BookingSystem.Domain.Common;
using MediatR;

namespace BookingSystem.Application.Features.Auth.Commands.LoginUser;

public class LoginUserCommand: IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}