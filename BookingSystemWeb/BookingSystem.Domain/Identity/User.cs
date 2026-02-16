using BookingSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity; 
namespace BookingSystem.Domain;

public class User: IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    public string? ExternalId { get; set; }
}