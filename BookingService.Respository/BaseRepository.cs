using BookingService.Model.Entities;
using BookingService.Respository.Contracts;
using BookingService.Respository.Context;
using System.Diagnostics.CodeAnalysis;

namespace BookingService.Respository
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly BookingContext _context;

        public BaseRepository(BookingContext context)
            : base()
        {
            this._context = context;
        }
    }
}