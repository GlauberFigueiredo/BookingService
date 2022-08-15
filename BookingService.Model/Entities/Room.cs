
using BookingService.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Model.Entities
{
    [Table("Room")]
    public class Room : BaseEntity
    {
        [Column("Floor")]
        public int Floor { get; set; }
        [Column("Number")]
        public int Number { get; set; }
        [Column("Type")]
        public RoomType Type { get; set; }
        [Column("Status")]
        public RoomStatus Status { get; set; }

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}