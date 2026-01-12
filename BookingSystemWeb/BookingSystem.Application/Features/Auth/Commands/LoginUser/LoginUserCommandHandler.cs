using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Domain;
using BookingSystem.Domain.Common;
using MediatR;

namespace BookingSystem.Application.Features.Auth.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
{
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator)
    {
        _identityService = identityService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _identityService.GetUserByEmailAsync(request.Email);
        if (user == null || !await _identityService.CheckPasswordAsync(user, request.Password))
        {
            return Result<string>.Failure("Invalid credentials");
        }

        var roles = await _identityService.GetUserRolesAsync(user);

        var token = _jwtTokenGenerator.GenerateToken(user, roles.ToList());

        return Result<string>.Success(token);
    }
}