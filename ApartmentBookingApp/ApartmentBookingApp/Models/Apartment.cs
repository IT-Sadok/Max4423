namespace ApartmentBookingApp
{
    public class Apartment
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; init; }
    }
}