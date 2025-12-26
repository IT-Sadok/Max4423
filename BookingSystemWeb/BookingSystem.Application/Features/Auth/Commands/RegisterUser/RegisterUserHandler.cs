using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Domain;
using MediatR;
using BookingSystem.Domain;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IIdentityService _identityService;
    public RegisterUserHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _identityService.IsEmailUniqueAsync(request.Email))
        {
            throw new Exception("User with this email is already registered.");
        }

        var (success, userId, errorMessage) = await _identityService.CreateUserAsync(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName,
            Roles.Client.ToString()
        );

        if (!success)
        {
            throw new Exception($"Registration failed: {errorMessage}");
        }

        return userId;
    }   
}