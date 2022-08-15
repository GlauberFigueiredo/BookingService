using BookingService.Model.Entities;
using BookingService.Respository.Context;
using BookingService.Respository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Respository
{
    public class RoomRepository : BaseRepository<Room>, IRoomRepository
    {
        public RoomRepository(BookingContext context)
            : base(context)
        { }

        public async Task<Room> GetById(Guid roomId)
        {
            return await _context.Rooms.FindAsync(roomId);
        }


    }
}
