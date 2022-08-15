using AutoMapper;
using NLog;

namespace BookingService.Service
{
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
