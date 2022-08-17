using BookingService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Respository.Contracts
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAll();
        Task<Reservation> GetById(Guid reservationId);
        Task<Reservation> Create(Reservation reservation);
        Task<Reservation> Update(Reservation reservation);
        Task<List<Reservation>> ListActiveByRoomAndDateRangeOverlap(DateOnly startDate,
                                                                    DateOnly endDate);
    }
}
