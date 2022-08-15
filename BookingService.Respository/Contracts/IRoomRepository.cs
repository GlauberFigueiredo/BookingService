using BookingService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Respository.Contracts
{
    public interface IRoomRepository
    {
        Task<Room> GetById(Guid roomId);
    }
}
