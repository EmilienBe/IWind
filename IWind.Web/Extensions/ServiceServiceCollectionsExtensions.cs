using System;
using System.Reflection;
using IWind.Web.Services.Address;
using IWind.Web.Services.Favorite;
using IWind.Web.Services.Weather;
using Microsoft.Extensions.DependencyInjection;

namespace IWind.Web.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddHelpersServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(Startup)));
            services.AddHttpClient<IAddressService,AddressService>(c => c.BaseAddress = new Uri("https://api-adresse.data.gouv.fr/"));
            
            services.AddHttpClient<IWeatherService, WeatherService>();
            services.AddScoped<IFavoriteLocationService, FavoriteLocationService>();
            return services;
        }
    }
}