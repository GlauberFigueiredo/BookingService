using BookingService.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Service.Contracts
{
    public interface IReservationService
    {
        Task<List<ReservationViewModel>> GetAll();

        Task<ReservationViewModel> CreateNew(DateOnly startDate,
                                             DateOnly endDate);

        Task<ReservationViewModel> Update(Guid reservationId,
                                          DateOnly startDate,
                                          DateOnly endDate);

        Task<ReservationViewModel> Cancel(Guid reservationId);

        Task<bool> IsAvailable(DateOnly startDate,
                               DateOnly endDate);
    }
}
