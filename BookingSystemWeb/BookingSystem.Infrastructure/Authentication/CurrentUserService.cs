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
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                return null;
            }
            var idClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(idClaim))
            {
                return null;
            }

            return Guid.Parse(idClaim);
        }
        
    }
}