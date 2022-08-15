using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Service
{
    public class RoomException : Exception
    {
        public RoomException(string message)
            : base(message)
        {
        }
    }
    public class DateException : Exception
    {
        public DateException(string message)
            : base(message)
        {
        }
    }
    public class OverbookingException : Exception
    {
        public OverbookingException(string message)
            : base(message)
        {
        }
    }
}
