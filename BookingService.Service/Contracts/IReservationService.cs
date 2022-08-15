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

        Task<ReservationViewModel> CreateNew(Guid roomId,
                                             DateOnly startDate,
                                             DateOnly endDate);
    }
}
