using BookingSystem.Domain;
using BookingSystem.Domain.Common;

namespace BookingSystem.Application.Common.Interfaces.Authentication;

public interface IIdentityService
{
    Task<bool> IsEmailUniqueAsync(string email);
    
    Task<Result<Guid>> CreateUserAsync(string email, string password, string firstName, string lastName, string role);
    
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<IList<string>> GetUserRolesAsync(User user);
}