using System.Diagnostics.CodeAnalysis;

namespace BookingService.Shared
{
    [ExcludeFromCodeCoverage]
    public static class AppFields
    {
        [ExcludeFromCodeCoverage]
        public static class ReservationStatus
        {
            public const string ACTIVE = "ACTIVE";
            public const string CANCELED = "CANCELED";
        }

        [ExcludeFromCodeCoverage]
        public static class RoomStatus
        {
            public const string AVAILABLE = "AVAILABLE";
            public const string UNAVAILABLE = "UNAVAILABLE";
        }
    }
}