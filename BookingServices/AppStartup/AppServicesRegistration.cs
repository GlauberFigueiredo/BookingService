using BookingService.Respository.Contracts;
using BookingService.Respository;
using BookingService.Service.Contracts;
using BookingService.Service;
using System.Diagnostics.CodeAnalysis;

namespace BookingService.AppStartup
{
    [ExcludeFromCodeCoverage]
    public class AppServicesRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region repository
            services.AddTransient<IReservationRepository, ReservationRepository>();
            #endregion

            #region services
            services.AddTransient<IReservationService, ReservationService>();
            #endregion
        }
    }
}
