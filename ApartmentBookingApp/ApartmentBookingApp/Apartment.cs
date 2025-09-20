namespace ApartmentBookingApp
{
    internal class Apartment
    {
        private static int _nextId = 1;
        public int Id { get; private set; }
        public string Title { get; set; }

        public decimal PricePerNight { get; set; }

        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public Apartment(string Title, decimal PricePerNight, int Capacity)
        {
            Id = _nextId++;
            this.Title = Title;
            this.PricePerNight = PricePerNight;
            this.Capacity = Capacity;
        }
    }
}