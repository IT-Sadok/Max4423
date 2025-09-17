using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentBookingApp
{
	internal class BookingSystem
	{
		static List<Host> hosts = new List<Host>();

		public static void AddHost()
		{
			int Id;
			while (true)
			{
				Console.Write("Enter host`s Id: ");
				if (int.TryParse(Console.ReadLine(), out Id) && Id > 0)
					break;
				Console.WriteLine("Invalid host`s Id. Please enter positive integer value.");
			}

			string FullName;

			while (true)
			{
				Console.Write("Enter host`s fullname: ");
				FullName = Console.ReadLine();

				if (!string.IsNullOrWhiteSpace(FullName))
					break;
				Console.WriteLine("Fullname cannot be null or empty.");
			}

			string PhoneNumber;

			while (true)
			{
				Console.Write("Enter host`s phone number: ");

				PhoneNumber = Console.ReadLine();
				if (PhoneNumber.Length > 5 && PhoneNumber.Length < 15)
					break;
				Console.WriteLine("Phone number cannot be < 5 and > 15 symbols");
			}

			hosts.Add(new Host(Id, FullName, PhoneNumber));
		}

		public static void ShowHosts()
		{
			if (hosts.Count > 0)
			{
				for (int i = 0; i < hosts.Count; i++)
				{
					Console.Write($"[{i + 1}]. ");
					hosts[i].ShowInfo();
					Console.Write("\n");
				}
			}

			else
				Console.WriteLine("Hosts list is empty!");
		}

		public static void AddApartmentToHost()
		{
			if (hosts.Count == 0)
			{
				Console.WriteLine("Hosts list is empty!");
				return;
			}
			int hostNumber;

			BookingSystem.ShowHosts();
			Console.Write($"Please, select host number(1 - {hosts.Count}): ");
			while (true)
			{
				if ((int.TryParse(Console.ReadLine(), out hostNumber) && hostNumber > 0 && hostNumber <= hosts.Count))
					break;

				Console.WriteLine("Incorrect host number. Please reenter.");
			}



			int Id;
			while (true)
			{
				Console.Write("Enter apartment Id: ");
				if (int.TryParse(Console.ReadLine(), out Id) && Id > 0)
					break;
				Console.WriteLine("Invalid apartment Id. Please enter positive integer value.");
			}

			string Title;

			while (true)
			{
				Console.Write("Enter apartment Title: ");
				Title = Console.ReadLine();

				if (!string.IsNullOrWhiteSpace(Title))
					break;
				Console.WriteLine("Title cannot be null or empty.");
			}

			decimal PricePerNight;

			while (true)
			{
				Console.Write("Enter price per night: ");
				if (decimal.TryParse(Console.ReadLine(), out PricePerNight))
					break;
				Console.WriteLine("Please, enter correct decimal value.");
			}

			int Capacity;
			while (true)
			{
				Console.Write("Enter apartment capacity: ");
				if (int.TryParse(Console.ReadLine(), out Capacity) && Capacity > 0)
					break;
				Console.WriteLine("Invalid apartment capacity. Please enter positive integer value.");
			}

			hosts[hostNumber - 1].Apartments.Add(new Apartment(Id, Title, PricePerNight, Capacity));

		}

		static int HostNumber;
		public static void ShowApartmentByHostId()
		{

			if (hosts.Count > 0)
			{
				for (int i = 0; i < hosts.Count; i++)
				{
					Console.Write($"[{i + 1}]. ");
					hosts[i].ShowInfo();
					Console.Write("\n");
				}
			}
			else
			{
				Console.WriteLine("There is no hosts!");
				return;
			}
			while (true)
			{
				Console.Write("Enter host`s numder: ");
				if (int.TryParse(Console.ReadLine(), out HostNumber) && HostNumber > 0 && HostNumber <= hosts.Count)
					break;
				Console.WriteLine("Invalid host`s number. Please reenter.");
			}

			if (hosts[HostNumber - 1].Apartments.Count == 0)
			{
				Console.WriteLine("This is host without apartments!");
				return;
			}
			for (int i = 0; i < hosts[HostNumber - 1].Apartments.Count; i++)
			{

				Console.WriteLine($"{i + 1}. Id: {hosts[HostNumber - 1].Apartments[i].Id} | Title: {hosts[HostNumber - 1].Apartments[i].Title} | PricePerNight: {hosts[HostNumber - 1].Apartments[i].PricePerNight} | Capacity: {hosts[HostNumber - 1].Apartments[i].Capacity} | IsAvailable: {hosts[HostNumber - 1].Apartments[i].IsAvailable}");

			}
		}

		public static void BookApartment()
		{
			ShowApartmentByHostId();
			if (hosts.Count == 0)
				return;

			if (hosts[HostNumber - 1].Apartments.Count == 0)
				return;

			Console.Write($"Enter {hosts[HostNumber - 1].FullName} host`s apartment number (1 - {hosts[HostNumber - 1].Apartments.Count}): ");

			int apartmentNumber;
			while (true)
			{
				if (int.TryParse(Console.ReadLine(), out apartmentNumber) &&
					apartmentNumber > 0 &&
					apartmentNumber <= hosts[HostNumber - 1].Apartments.Count)
				{
					break;
				}
				Console.WriteLine("Invalid apartment number. Please reenter.");
			}

			var apartment = hosts[HostNumber - 1].Apartments[apartmentNumber - 1];

			if (apartment.IsAvailable)
			{
				apartment.IsAvailable = false;
				Console.WriteLine($"Apartment #{apartmentNumber} in {hosts[HostNumber - 1].FullName} is booked!");
			}
			else
			{
				Console.WriteLine("This apartment was already booked.");
			}
		}

	}
}
