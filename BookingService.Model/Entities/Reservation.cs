using BookingService.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Model.Entities
{
    [Table("Reservation")]
    public class Reservation : BaseEntity
    {
        public Reservation(Room room, DateOnly startDate, DateOnly endDate)
        {
            this.Id = Guid.NewGuid();
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Status = ReservationStatus.ACTIVE;
            RoomId = room.Id;
            this.Room = room;
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

        [ForeignKey("RoomId")]
        public Guid RoomId { get; set; }

        public virtual Room Room { get; set; } = new Room();

    }
}