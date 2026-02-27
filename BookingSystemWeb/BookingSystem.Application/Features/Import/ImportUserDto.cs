using System.Text.Json.Serialization;

namespace BookingSystem.Application.Features.Import.DTOs;

public class ImportUserDto
{
    [JsonPropertyName("id")]
    public string ExternalId { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("apartments")]
    public List<ImportApartmentDto> Apartments { get; set; } = new();
}