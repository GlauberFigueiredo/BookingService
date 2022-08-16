using BookingService.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Respository.Context
{
    [ExcludeFromCodeCoverage]
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options)
          : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies();
        }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
