
using BookingService.Model.Enums;

namespace BookingService.Model.Entities
{
    public class Room : BaseEntity
    {
        public int Floor { get; set; }
        public int Number { get; set; }
        public RoomType Type { get; set; }
        public RoomStatus Status { get; set; }
    }
}