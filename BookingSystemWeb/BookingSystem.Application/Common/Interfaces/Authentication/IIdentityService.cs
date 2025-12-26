namespace BookingSystem.Application.Common.Interfaces.Authentication;

public interface IIdentityService
{
    Task<bool> IsEmailUniqueAsync(string email);
    
    Task<(bool Success, Guid UserId, string? ErrorMessage)> CreateUserAsync(string email, string password, string firstName, string lastName, string role);
}