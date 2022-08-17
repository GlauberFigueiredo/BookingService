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
        
        public async Task<Reservation> GetById(Guid reservationId)
        {
            return await _context.Reservations.FindAsync(reservationId);
        }

        public async Task<List<Reservation>> ListActiveByRoomAndDateRangeOverlap(DateOnly startDate, DateOnly endDate)
        {
            var reservations = await _context.Reservations
                .Where(y => y.StartDate <= endDate && startDate <= y.EndDate)
                .Where(z => z.Status == Model.Enums.ReservationStatus.ACTIVE)
                .ToListAsync();

            return reservations;
        }

        public async Task<Reservation> Create(Reservation reservation)
        {
            await _context.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        public async Task<Reservation> Update(Reservation reservation)
        {
            var entity = _context.Reservations.Attach(reservation);
            entity.State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return reservation;
        }

    }
}
