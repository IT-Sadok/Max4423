using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Domain;
using BookingSystem.Domain.Common;
using MediatR;

namespace BookingSystem.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _identityService.IsEmailUniqueAsync(request.Email))
        {
            return Result<Guid>.Failure("User with this email is already registered.");
        }

        var result = await _identityService.CreateUserAsync(
            request.Email,
            request.Password,
            request.FirstName,
            request.LastName, 
            Roles.Client
        );
        
        return result;
    }   
}