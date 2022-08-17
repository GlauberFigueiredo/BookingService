using BookingService.Respository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Respository.Context
{
    public static class DatabaseSeeder
    {
        public static void PopulateInMemoryDatabase(BookingContext context)
        {
            if (context.Reservations.Any())
                context.Reservations.RemoveRange(context.Reservations.ToList());

            var reservation1 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("f1a7c449-323b-492c-b902-b43150ab0b7d"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            var reservation2 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("329def49-4d09-4bfb-96ae-68ee5878d313"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(8)),
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            var reservation3 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("e2b76ff4-6bb9-4a11-9524-557a4e2f100d"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(13)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(16)),
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };
            var reservation4 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("a8301865-454e-4d92-b60f-1f23bbfe7fd2"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(17)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(18)),
                Status = BookingService.Model.Enums.ReservationStatus.CANCELLED
            };
            var reservation5 = new BookingService.Model.Entities.Reservation
            {
                Id = Guid.Parse("656208a2-639a-4816-888a-e5e53f111d24"),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(23)),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(25)),
                Status = BookingService.Model.Enums.ReservationStatus.ACTIVE
            };

            var reservationsList = new List<BookingService.Model.Entities.Reservation> { reservation1, reservation2, reservation3, reservation4, reservation5 };

            context.Reservations.AddRange(reservationsList);

            context.SaveChanges();

        }
    }
}
