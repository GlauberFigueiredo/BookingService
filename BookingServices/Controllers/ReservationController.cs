using Microsoft.AspNetCore.Mvc;
using BookingService.Service.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BookingService.Model.ViewModels;

namespace BookingService.Controllers
{
    [Route("reservations")]
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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var response = new
                {
                    data = await _reservationService.GetAll()
                };
                return Ok(response);
            }
            catch (Exception exception)
            {
                return StatusCode(500, FormatExceptionToReturn(exception));
            }
        }

        [HttpPost]
        [Route("{reservationId}")]
        public async Task<IActionResult> Update([FromRoute] Guid reservationId)
        {
            if (reservationId == Guid.Empty)
                return BadRequest($"BadRequest: An invalid Id was informed.");

            try
            {
                var response = new
                {
                    data = await _reservationService.GetAll()
                };
                return Ok(response);
            }
            catch (Exception exception)
            {
                return StatusCode(500, FormatExceptionToReturn(exception));
            }
        }

        [HttpPost]
        [Route("new/{roomId}")]
        public async Task<IActionResult> Create([FromRoute] Guid roomId,
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            if (roomId == Guid.Empty)
                return BadRequest($"BadRequest: An invalid room was informed.");
            if (string.IsNullOrEmpty(startDate.ToString())
                || string.IsNullOrEmpty(endDate.ToString()))
                return BadRequest($"BadRequest: An invalid date range was informed.");

            try
            {
                var response = new
                {
                    data = await _reservationService.CreateNew(roomId, DateOnly.FromDateTime(startDate), DateOnly.FromDateTime(endDate))
                };
                return Created("", response);
            }
            catch (Exception exception)
            {
                return StatusCode(500, FormatExceptionToReturn(exception));
            }
        }


        private string FormatExceptionToReturn(Exception exception)
        {
            var errorString = $"{exception.Message}";
            var errorResponse = new JObject(
                new JProperty("message", new JValue(errorString)),
                new JProperty("stackTrace", new JValue(exception.StackTrace))
                );

            return errorResponse.ToString();
        }


    }
}