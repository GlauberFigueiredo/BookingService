using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Shared
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
        : base(message)
            {
            }
    }
    public class DateValidationException : BadRequestException
    {
        public DateValidationException(string message)
            : base(message)
        {
        }
    }
    public class OverbookingException : BadRequestException
    {
        public OverbookingException(string message)
            : base(message)
        {
        }
    }
    public class ReservationValidationException : BadRequestException
    {
        public ReservationValidationException(string message)
            : base(message)
        {
        }
    }
    public class DateOnlyConverterException : BadRequestException
    {
        public DateOnlyConverterException(string message)
            : base(message)
        {
        }
    }
}
