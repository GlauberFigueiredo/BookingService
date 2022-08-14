using Microsoft.AspNetCore.Mvc;
using BookingService.Service.Contracts;

namespace BookingService.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        #region Properties
        private readonly IReservationService _reservationService;
        #endregion

        #region Constructor
        public ReservationController(IReservationService reservationService)
        {
            this._reservationService = reservationService;
         }
        #endregion



    }
}