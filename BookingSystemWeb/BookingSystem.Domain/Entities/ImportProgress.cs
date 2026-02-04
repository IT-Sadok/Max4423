namespace BookingSystem.Domain.Entities;

public class ImportProgress
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = string.Empty;
    public string LastProcessedExternalId { get; set; } = string.Empty;
    public int ProcessedCount { get; set; }
    public DateTime UpdatedAt { get; set; }
}