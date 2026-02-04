using System.Text.Json.Serialization;

namespace BookingSystem.Application.Features.Import.DTOs;

public class ImportApartmentDto
{
    [JsonPropertyName("id")]
    public string ExternalId { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
    
    [JsonPropertyName("price")]
    public decimal PricePerNight { get; set; }
}