using BookingService.Model.Enums;

namespace BookingService.Model.Entities
{
    public class Reservation : BaseEntity
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Room Room { get; set; }
        public ReservationStatus Status { get; set; }
    }
}