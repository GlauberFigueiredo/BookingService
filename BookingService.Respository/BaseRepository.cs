using BookingService.Model.Entities;
using BookingService.Respository.Contracts;
using BookingService.Respository.Context;

namespace BookingService.Respository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly BookingContext _context;

        public BaseRepository(BookingContext context)
            : base()
        {
            _context = context;
        }
        public Task<Guid> Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}