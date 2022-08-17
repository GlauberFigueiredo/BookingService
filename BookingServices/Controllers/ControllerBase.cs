using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using NLog;

namespace BookingServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ExcludeFromCodeCoverage]
    public class ControllerBase
    {
        protected static Logger _logger = LogManager.GetCurrentClassLogger();

        protected ControllerBase() { }
    }
}
