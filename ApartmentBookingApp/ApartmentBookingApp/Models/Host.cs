namespace ApartmentBookingApp
{
	internal class Host
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public List<Apartment> Apartments = new List<Apartment>();
	}

}
