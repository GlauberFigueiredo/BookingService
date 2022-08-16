using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Shared
{
    public static class DateOnlyConverter
    {
        public static DateOnly DateTimeToDateOnlyConverter(DateTime date)
        {
            try
            {
                return DateOnly.FromDateTime(date);
            }
            catch (Exception)
            {
                throw new DateOnlyConverterException("An invalid date was informed.");
            }
        }
    }
}
