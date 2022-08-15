using AutoMapper;

namespace BookingService.AppStartup
{
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
