using BookingSystem.Application.Common.Interfaces.Authentication;
using BookingSystem.Domain;
using BookingSystem.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Infrastructure.Authentication;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public IdentityService(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user == null;
    }

    public async Task<Result<Guid>> CreateUserAsync(string email, string password, string firstName, string lastName,
        string role)
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
            return Result<Guid>.Failure($"Registration failed: {errors}");
        }

        if (!await _roleManager.RoleExistsAsync(role))
        {
            var createRoleResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            if (!createRoleResult.Succeeded)
            {
                return Result<Guid>.Failure("Failed to create role in database.");
            }
        }
        
        var roleResult = await _userManager.AddToRoleAsync(user, role);

        if (!roleResult.Succeeded)
        {
            return Result<Guid>.Failure("User created but failed to assign role.");
        }

        return Result<Guid>.Success(user.Id);
    }
    
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IList<string>> GetUserRolesAsync(User user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}