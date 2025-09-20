namespace ApartmentBookingApp
{
	internal class Host
	{
		private static int _nextId = 1;
		public int Id { get; private set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public List<Apartment> Apartments = new List<Apartment>();
		public Host(string FullName, string PhoneNumber)
		{
			Id = _nextId++;;
			this.FullName = FullName;
			this.PhoneNumber = PhoneNumber;
		}

		public void ShowInfo()
		{
			Console.Write($"Id: {Id} | FullName: {FullName} | PhoneNumber: {PhoneNumber}\n");
		}
	}

}
