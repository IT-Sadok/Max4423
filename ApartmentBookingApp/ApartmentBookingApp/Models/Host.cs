namespace ApartmentBookingApp
{
    public class Host
    {
        public int Id { get; init; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}