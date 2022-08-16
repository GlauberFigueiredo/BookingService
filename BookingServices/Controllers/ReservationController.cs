using Microsoft.AspNetCore.Mvc;
using BookingService.Service.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BookingService.Model.ViewModels;
using BookingService.Shared;

namespace BookingService.Controllers
{
    [Route("reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this._reservationService = reservationService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
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
        [Route("check_availability")]
        public async Task<IActionResult> CheckAvailability([FromQuery] DateTime startDate,
                                                   [FromQuery] DateTime endDate)
        {
            try
            {
                var startDateOnly = DateOnlyConverter.DateTimeToDateOnlyConverter(startDate);
                var endDateOnly = DateOnlyConverter.DateTimeToDateOnlyConverter(endDate);

                var response = new
                {
                    data = await _reservationService.IsAvailable(startDateOnly, endDateOnly)
                };
                return Ok(response);
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, FormatExceptionToReturn(exception));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] DateTime startDate,
                                                [FromQuery] DateTime endDate)
        {
            try
            {
                var startDateOnly = DateOnlyConverter.DateTimeToDateOnlyConverter(startDate);
                var endDateOnly = DateOnlyConverter.DateTimeToDateOnlyConverter(endDate);

                var response = new
                {
                    data = await _reservationService.CreateNew(startDateOnly, endDateOnly)
                };
                //TODO check return
                return Created("", response);
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, FormatExceptionToReturn(exception));
            }
        }

        [HttpPost]
        [Route("{reservationId}")]
        public async Task<IActionResult> Modify([FromRoute] Guid reservationId,
                                                [FromQuery] DateTime startDate,
                                                [FromQuery] DateTime endDate)
        {
            if (reservationId == Guid.Empty)
                return BadRequest($"BadRequest: An invalid reservation was informed.");
            
            try
            {
                var startDateOnly = DateOnlyConverter.DateTimeToDateOnlyConverter(startDate);
                var endDateOnly = DateOnlyConverter.DateTimeToDateOnlyConverter(endDate);

                var response = new
                {
                    data = await _reservationService.Update(reservationId, startDateOnly, endDateOnly)
                };
                return Ok(response);
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, FormatExceptionToReturn(exception));
            }
        }

        [HttpPost]
        [Route("cancel/{reservationId}")]
        public async Task<IActionResult> Cancel([FromRoute] Guid reservationId)
        {
            if (reservationId == Guid.Empty)
                return BadRequest($"BadRequest: An invalid Id was informed.");

            try
            {
                var response = new
                {
                    data = await _reservationService.Cancel(reservationId)
                };
                return Ok(response);
            }
            catch (BadRequestException exception)
            {
                return BadRequest(exception.Message);
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