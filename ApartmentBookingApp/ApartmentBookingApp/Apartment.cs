using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentBookingApp
{
	internal class Apartment
	{
		public int Id { get; set; }
		public string Title { get; set; }

		public decimal PricePerNight { get; set; }

		public int Capacity { get; set; }

		public bool IsAvailable { get; set; } = true;

		public Apartment(int Id, string Title, decimal PricePerNight, int Capacity)
		{
			this.Id = Id;
			this.Title = Title;
			this.PricePerNight = PricePerNight;
			this.Capacity = Capacity;
		}
	}
}
