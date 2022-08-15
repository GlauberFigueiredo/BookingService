using BookingService.Respository.Contracts;
using BookingService.Respository;
using BookingService.Service.Contracts;
using BookingService.Service;

namespace BookingService.AppStartup
{
    public class AppServicesRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            #region repository
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IRoomRepository, RoomRepository>();
            #endregion

            #region services
            services.AddTransient<IReservationService, ReservationService>();
            #endregion
        }
    }
}
