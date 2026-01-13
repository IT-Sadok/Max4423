using System.Reflection;
using BookingSystem.Application.Features.Apartments.Queries.SearchApartments;
using BookingSystem.Domain.Entities;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Infrastructure.Common.Mapping;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetAssembly(typeof(ApartmentDto))!);
        config.NewConfig<Apartment, ApartmentDto>()
            .Map(dest => dest.HostName, src => $"{src.Host.FirstName} {src.Host.LastName}");
    }
}