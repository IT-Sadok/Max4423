namespace ApartmentBookingApp
{
    public class Apartment
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public decimal PricePerNight { get; init; }
        public int Capacity { get; init; }
    }
}