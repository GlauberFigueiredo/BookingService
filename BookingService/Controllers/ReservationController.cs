using System;
using Microsoft.AspNetCore.Mvc;


namespace BookingService.Controllers
{
    public class ReservationController
    {
        #region Properties
        private readonly IReservationService _reservationService;
        #endregion

        public ReservationController(IReservationService reservationService)
        {
               this._reservationService = reservationService;
        }
    }
}
