using AutoMapper;
using NLog;
using System.Diagnostics.CodeAnalysis;

namespace BookingService.Service
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseService
    {
        protected static Logger _logger = LogManager.GetCurrentClassLogger();
        protected readonly IMapper _mapper;

        public BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
