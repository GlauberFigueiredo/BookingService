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
        Task<List<Reservation>> ListActiveByRoomAndDateRangeOverlap(Guid roomId, DateOnly startDate, DateOnly endDate);
    }
}
