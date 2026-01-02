using BookingSystem.Domain;

namespace BookingSystem.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user, List<string> roles);
}