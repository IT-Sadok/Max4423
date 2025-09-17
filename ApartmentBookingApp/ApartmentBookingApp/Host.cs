using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentBookingApp
{
	internal class Host
	{
		public int Id { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public List<Apartment> Apartments = new List<Apartment>();
		public Host(int Id, string FullName, string PhoneNumber)
		{
			this.Id = Id;
			this.FullName = FullName;
			this.PhoneNumber = PhoneNumber;
		}

		public void ShowInfo()
		{
			Console.Write($"Id: {Id} | FullName: {FullName} | PhoneNumber: {PhoneNumber}\n");
		}
	}

}
