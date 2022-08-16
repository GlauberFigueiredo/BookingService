using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace BookingService.AppStartup
{
    [ExcludeFromCodeCoverage]
    public static class AutomapperStartupConfig
    {
        public static List<Profile> ListOfAutomapperProfiles => new List<Profile>()
        {
            new Model.MappingProfiles.ReservationProfile()
        };

        public static void AddAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(configuration => configuration.AddProfiles(ListOfAutomapperProfiles));
        }
    }
}
