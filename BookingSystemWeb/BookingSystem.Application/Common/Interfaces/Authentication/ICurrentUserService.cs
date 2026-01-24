namespace BookingSystem.Application.Common.Interfaces.Authentication;

public interface ICurrentUserService
{
    public Guid? UserId { get; }
}