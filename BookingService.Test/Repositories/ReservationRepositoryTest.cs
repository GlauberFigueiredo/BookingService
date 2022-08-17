using BookingService.Model.Enums;
using BookingService.Respository;
using BookingService.Respository.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Test
{
    internal class ReservationRepositoryTest
    {
        private DbContextOptions<BookingContext> _dbContextTestOptions;
        private BookingContext _testContext;

        private ReservationRepository _reservationTestRepository;

        public ReservationRepositoryTest()
        {
            _dbContextTestOptions = new DbContextOptionsBuilder<BookingContext>()
               .UseInMemoryDatabase("InMemoryRepositoryTestDatabase")
               .Options;

            _testContext = new BookingContext(_dbContextTestOptions);

            this._reservationTestRepository = new ReservationRepository(_testContext);
        }


        [SetUp]
        public void Setup()
        {
            DatabaseSeeder.PopulateInMemoryDatabase(_testContext);
        }

        [Test]
        public void ListAllTest()
        {
            Assert.DoesNotThrowAsync(async () => await _reservationTestRepository.GetAll());
        }
        [Test]
        public void GetByIdTest()
        {
            Assert.DoesNotThrowAsync(async () => await _reservationTestRepository.GetById(Guid.NewGuid()));
        }
        [Test]
        public void CreateTest()
        {
            Assert.DoesNotThrowAsync(async () => await _reservationTestRepository.Create(new Model.Entities.Reservation()));
        }
        [Test]
        public async Task UpdateTest()
        {
            var reservationToBeUpdated = await _reservationTestRepository.GetById(Guid.Parse("656208a2-639a-4816-888a-e5e53f111d24"));
            reservationToBeUpdated.Status = ReservationStatus.CANCELLED;
            Assert.DoesNotThrowAsync(() => _reservationTestRepository.Update(reservationToBeUpdated));
        }
        [Test]
        public void ListActiveByRoomAndDateRangeOverlapTest()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(23));
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(25));
            Assert.DoesNotThrowAsync(async () => await _reservationTestRepository.ListActiveByRoomAndDateRangeOverlap(startDate, endDate));
        }
    }
}
