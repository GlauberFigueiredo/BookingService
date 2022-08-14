using BookingService.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Respository.Contracts
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<Guid> Create(TEntity entity);
        Task Delete(Guid id);
        Task Update(TEntity entity);

    }
}
