using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Application.Features.Auth.Commands.LoginUser;

public class LoginUserHandler: IRequestHandler<LoginUserCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserHandler(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new Exception("Invalid credentials"); 
        }
        
        var roles = await _userManager.GetRolesAsync(user);
        
        var token = _jwtTokenGenerator.GenerateToken(user, roles.ToList());
        
        return token;
    }
}