using AutoMapper;
using BookingService.Model.Enums;
using BookingService.Respository;
using BookingService.Respository.Context;
using BookingService.Respository.Contracts;
using BookingService.Service;
using BookingService.Shared;
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
    internal class ReservationServiceTest
    {
        private ReservationService _reservationServiceTest;
        private DbContextOptions<BookingContext> _dbContextTestOptions;
        private BookingContext _testContext;
        private ReservationRepository _reservationTestRepository;

        public ReservationServiceTest()
        {
            _dbContextTestOptions = new DbContextOptionsBuilder<BookingContext>()
                           .UseInMemoryDatabase("InMemoryServiceTestDatabase")
                           .Options;

            _testContext = new BookingContext(_dbContextTestOptions);

            _reservationTestRepository = new ReservationRepository(_testContext);

            var mapperMock = new Mock<IMapper>();

            this._reservationServiceTest = new ReservationService(reservationRepository: _reservationTestRepository,
                                                                  mapper: mapperMock.Object);
        }

        [SetUp]
        public void Setup()
        {
            DatabaseSeeder.PopulateInMemoryDatabase(_testContext);
        }

        [Test]
        public void ListAllTest_ShouldRunWithoutException()
        {
            Assert.DoesNotThrowAsync(async () => await _reservationServiceTest.GetAll());
        }
        
        [Test]
        public void IsAvailableTest_ShouldRunWithoutException()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
            Assert.DoesNotThrowAsync(async () => await _reservationServiceTest.IsAvailable(startDate, endDate));
        }
        [Test]
        public void IsAvailable_ShouldThrowDateException()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
            Assert.ThrowsAsync<DateValidationException>(async () => await _reservationServiceTest.CreateNew(startDate, endDate));
        }
        [TestCase(true)]
        public async Task IsAvailableTest_ShouldReturnTrue(bool expected)
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(28));
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(29));

            var result = await _reservationServiceTest.IsAvailable(startDate, endDate);

            Assert.AreEqual(expected, result);
        }
        [TestCase(false)]
        public async Task IsAvailableTest_ShouldReturnFalse(bool expected)
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3));
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5));

            var result = await _reservationServiceTest.IsAvailable(startDate, endDate);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CreateNew_ShouldRunWithoutException()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
            Assert.DoesNotThrowAsync(async () => await _reservationServiceTest.CreateNew(startDate, endDate));
        }
        [Test]
        public void CreateNew_ShouldThrowDateException()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
            Assert.ThrowsAsync<DateValidationException>(async () => await _reservationServiceTest.CreateNew(startDate, endDate));
        }
        [Test]
        public void CreateNew_ShouldThrowOverbookingException()
        {
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3));
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5));
            Assert.ThrowsAsync<OverbookingException>(async () => await _reservationServiceTest.CreateNew(startDate, endDate));
        }

        [Test]
        public void Update_ShouldRunWithoutException()
        {
            var reservationId = Guid.Parse("329def49-4d09-4bfb-96ae-68ee5878d313");
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(9));
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(10));
            Assert.DoesNotThrowAsync(async () => await _reservationServiceTest.Update(reservationId, startDate, endDate));
        }
        [Test]
        public void Update_ShouldThrowDateException()
        {
            var reservationId = Guid.Parse("329def49-4d09-4bfb-96ae-68ee5878d313");
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
            Assert.ThrowsAsync<DateValidationException>(async () => await _reservationServiceTest.Update(reservationId, startDate, endDate));
        }
        [Test]
        public void Update_ShouldThrowOverbookingException()
        {
            var reservationId = Guid.Parse("329def49-4d09-4bfb-96ae-68ee5878d313");
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3));
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5));
            Assert.ThrowsAsync<OverbookingException>(async () => await _reservationServiceTest.Update(reservationId, startDate, endDate));
        }
        [Test]
        public void Update_ShouldThrowReservationException()
        {
            var reservationId = Guid.NewGuid();
            var startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(3));
            var endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5));
            Assert.ThrowsAsync<ReservationValidationException>(async () => await _reservationServiceTest.Update(reservationId, startDate, endDate));
        }

        [Test]
        public void Cancel_ShouldRunWithoutException()
        {
            var reservationId = Guid.Parse("656208a2-639a-4816-888a-e5e53f111d24");
            Assert.DoesNotThrowAsync(async () => await _reservationServiceTest.Cancel(reservationId));
        }
        [Test]
        public void Cancel_ShouldThrowReservationException()
        {
            var reservationId = Guid.NewGuid();
            Assert.ThrowsAsync<ReservationValidationException>(async () => await _reservationServiceTest.Cancel(reservationId));
        }
    }
}
