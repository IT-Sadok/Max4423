using BookingSystem.Domain;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly UserManager<User> _userManager;
    private readonly IValidator<RegisterUserCommand> _validator;

    public RegisterUserCommandHandler(UserManager<User> userManager,IValidator<RegisterUserCommand> validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            var error = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new Exception($"Validation failed: {error}");
        }
        var existingUser = await _userManager.FindByEmailAsync(request.Email);

        if (existingUser != null)
        {
            throw new Exception("User with this email is already registered.");
        }

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        
        if (!result.Succeeded)
        {
            throw new Exception($"Creating user failed. Error: {errors}");
        }
        
        return newUser.Id;
    }   
}