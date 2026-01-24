using System.Reflection;
using BookingSystem.Application.Features.Apartments.Queries.SearchApartments;
using BookingSystem.Application.Features.Bookings.Queries.GetMyBookings;
using BookingSystem.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Application.Common.Mapping;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetAssembly(typeof(ApartmentDto))!);
        config.NewConfig<Apartment, ApartmentDto>()
            .Map(dest => dest.HostName, src => $"{src.Host.FirstName} {src.Host.LastName}");
        config.NewConfig<Booking, BookingDto>()
            .Map(dest => dest.ApartmentTitle, src => src.Apartment.Title)
            .Map(dest => dest.ApartmentDescription, src => src.Apartment.Description)
            .Map(dest => dest.ApartmentAddress, src => src.Apartment.Address)
            .Map(dest => dest.Status, src => src.BookingStatus);
    }
}