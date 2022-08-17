using AutoMapper;
using BookingService.Controllers;
using BookingService.Model.Enums;
using BookingService.Respository;
using BookingService.Respository.Context;
using BookingService.Service;
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
    internal class ReservationControllerTest
    {
        private ReservationController _reservationControllerTest;
        private DbContextOptions<BookingContext> _dbContextTestOptions;
        private BookingContext _testContext;
        private ReservationRepository _reservationTestRepository;
        private ReservationService _reservationServiceTest;

        public ReservationControllerTest()
        {
            _dbContextTestOptions = new DbContextOptionsBuilder<BookingContext>()
               .UseInMemoryDatabase("InMemoryControllerTestDatabase")
               .Options;
            _testContext = new BookingContext(_dbContextTestOptions);


            var mapperMock = new Mock<IMapper>();
            _reservationTestRepository = new ReservationRepository(_testContext);

            _reservationServiceTest = new ReservationService(reservationRepository: _reservationTestRepository,
                                                                  mapper: mapperMock.Object);
            _reservationControllerTest = new ReservationController(_reservationServiceTest);

        }


        [SetUp]
        public void Setup()
        {
            DatabaseSeeder.PopulateInMemoryDatabase(_testContext);
        }

        [TestCase(200)]
        public void ListAllTest_Ok(int expected)
        {
            var result = (dynamic)_reservationControllerTest.ListAll().Result;
            Assert.AreEqual(expected, result.StatusCode);
        }

        [TestCase(200)]
        public void CheckAvailability_Ok(int expected)
        {
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);

            var result = (dynamic)_reservationControllerTest.CheckAvailability(startDate, endDate).Result;
            Assert.AreEqual(expected, result.StatusCode);
        }
        [TestCase(400)]
        public void CheckAvailability_BadRequest(int expected)
        {
            var startDate = DateTime.Now.AddDays(0);
            var endDate = DateTime.Now.AddDays(5);

            var result = (dynamic)_reservationControllerTest.CheckAvailability(startDate, endDate).Result;
            Assert.AreEqual(expected, result.StatusCode);
        }

        [TestCase(201)]
        public void Create_Created(int expected)
        {
            var startDate = DateTime.Now.AddDays(28);
            var endDate = DateTime.Now.AddDays(29);

            var result = (dynamic)_reservationControllerTest.Create(startDate, endDate).Result;
            Assert.AreEqual(expected, result.StatusCode);
        }
        [TestCase(400)]
        public void Create_BadRequest(int expected)
        {
            var startDate = DateTime.Now.AddDays(3);
            var endDate = DateTime.Now.AddDays(5);

            var result = (dynamic)_reservationControllerTest.Create(startDate, endDate).Result;
            Assert.AreEqual(expected, result.StatusCode);
        }

        [TestCase(200)]
        public void Modify_Ok(int expected)
        {
            var id = Guid.Parse("656208a2-639a-4816-888a-e5e53f111d24");
            var startDate = DateTime.Now.AddDays(28);
            var endDate = DateTime.Now.AddDays(29);

            var result = (dynamic)_reservationControllerTest.Modify(id, startDate, endDate).Result;
            Assert.AreEqual(expected, result.StatusCode);
        }
        [TestCase(400)]
        public void Modify_BadRequest(int expected)
        {
            var id = Guid.Parse("656208a2-639a-4816-888a-e5e53f111d24");
            var startDate = DateTime.Now.AddDays(3);
            var endDate = DateTime.Now.AddDays(5);

            var result = (dynamic)_reservationControllerTest.Modify(id, startDate, endDate).Result;
            Assert.AreEqual(expected, result.StatusCode);
        }

        [TestCase(200)]
        public void Cancel_Ok(int expected)
        {
            var id = Guid.Parse("656208a2-639a-4816-888a-e5e53f111d24");
            
            var result = (dynamic)_reservationControllerTest.Cancel(id).Result;
            Assert.AreEqual(expected, result.StatusCode);
        }
        [TestCase(400)]
        public void Cancel_BadRequest(int expected)
        {
            var id = Guid.Parse("a8301865-454e-4d92-b60f-1f23bbfe7fd2");

            var result = (dynamic)_reservationControllerTest.Cancel(id).Result;
            Assert.AreEqual(expected, result.StatusCode);
        }
    }
}
