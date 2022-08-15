using BookingService.Model.Entities;
using BookingService.Respository.Context;
using BookingService.Respository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Respository
{
    public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
    {
        public ReservationRepository(BookingContext context)
            : base(context)
        {

        }

        public async Task<List<Reservation>> GetAll()
        {
            var reservations = await _context.Reservations
                .ToListAsync();
            return reservations;
        }

        public async Task<List<Reservation>> ListActiveByRoomAndDateRangeOverlap(Guid roomId, DateOnly startDate, DateOnly endDate)
        {
            var reservations = await _context.Reservations
                .Where(x => x.RoomId == roomId)
                .Where(y => y.StartDate <= endDate && startDate <= y.EndDate )
                .Where(z => z.Status == Model.Enums.ReservationStatus.ACTIVE)
                .ToListAsync();

            return reservations;
        }

        public async Task<Reservation> Create(Reservation reservation)
        {
            if (GetById(reservation.Id) == null)
            {
                var result = await _context.AddAsync(reservation);
                _context.SaveChangesAsync();
            }


            return new Reservation();

        }

        private async Task<Reservation> GetById(Guid reservationId)
        {
            return await _context.Reservations.FindAsync(reservationId);
        }

    }
}
