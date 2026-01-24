using System.Security.Claims;
using BookingSystem.Application.Common.Interfaces.Authentication;
using Microsoft.AspNetCore.Http;

namespace BookingSystem.Infrastructure.Authentication;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(idClaim, out var userId))
            {
                return userId;
            }

            return null;
        }
    }
}