using BookingService.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BookingService.Model.Entities
{
    [Table("Reservation")]
    [ExcludeFromCodeCoverage]
    public class Reservation : BaseEntity
    {
        public Reservation(DateOnly startDate, DateOnly endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Status = ReservationStatus.ACTIVE;
        }

        public Reservation()
        {
        }

        [Column("StartDate")]
        public DateOnly StartDate { get; set; }
        [Column("EndDate")]
        public DateOnly EndDate { get; set; }
        [Column("Status")]
        public ReservationStatus Status { get; set; }
    }
}