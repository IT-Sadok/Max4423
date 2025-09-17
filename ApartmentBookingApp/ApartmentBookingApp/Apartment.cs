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

		public bool IsAvailable { get; set; }

	}
}
