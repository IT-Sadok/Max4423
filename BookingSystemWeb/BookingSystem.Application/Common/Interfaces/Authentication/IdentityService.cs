using BookingSystem.Domain;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Application.Common.Interfaces.Authentication;

public class IdentityService: IIdentityService
{
    private readonly UserManager<User> _userManager;
    public IdentityService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user == null;
    }

    public async Task<(bool Success, Guid UserId, string? ErrorMessage)> CreateUserAsync(string email, string password, string firstName, string lastName, string role)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            UserName = email,
            FirstName = firstName,
            LastName = lastName
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, Guid.Empty, errors);
        }
        
        var roleResult = await _userManager.AddToRoleAsync(user, role);
        
        if (!roleResult.Succeeded)
        {
            return (false, Guid.Empty, "User created but failed to assign role.");
        }

        return (true, user.Id, null);
    }
}