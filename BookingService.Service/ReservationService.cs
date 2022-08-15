using AutoMapper;
using BookingService.Model.Entities;
using BookingService.Model.ViewModels;
using BookingService.Respository.Contracts;
using BookingService.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Service
{
    public class ReservationService : BaseService, IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;
        public ReservationService(IReservationRepository reservationRepository
                                  , IRoomRepository roomRepository
                                  , IMapper mapper)
            : base(mapper)
        {
            this._reservationRepository = reservationRepository;
            this._roomRepository = roomRepository;
        }

        public async Task<ReservationViewModel> CreateNew(Guid roomId, DateOnly startDate, DateOnly endDate)
        {
            var selectedRoom = await _roomRepository.GetById(roomId);

            ValidateRoom(selectedRoom);
            ValidateSelectedDates(startDate, endDate);
            PreventOverbooking(roomId, startDate, endDate);



            return new ReservationViewModel();



        }

        public async Task<List<ReservationViewModel>> GetAll()
        {
            var reservations = await _reservationRepository.GetAll();
            var result = _mapper.Map<List<Reservation>, List<ReservationViewModel>>(reservations);
            return result;
        }

        private void ValidateRoom(Room room)
        {
            if (room == null)
                throw new RoomException("No room was found for the informed Id.");
            if (room.Status != Model.Enums.RoomStatus.AVAILABLE)
                throw new RoomException("Selected room is not available to be booked.");
        }
        private void ValidateSelectedDates(DateOnly startDate, DateOnly endDate)
        {
            var stayLimit = 3;
            var advanceReservationlimit = 30;

            var today = DateOnly.FromDateTime(DateTime.Now);

            if ((startDate.DayNumber - today.DayNumber) < 1)
                throw new DateException("All reservations must start at least the next day of booking.");
            if (endDate.DayNumber <= startDate.DayNumber)
                throw new DateException("The end date must be greater than the start date.");
            if ((startDate.DayNumber - today.DayNumber) > advanceReservationlimit)
                throw new DateException($"Rooms can`t be reserved more than {advanceReservationlimit} days in advance.");
            if ((endDate.DayNumber - startDate.DayNumber) > stayLimit)
                throw new DateException($"Rooms can`t be reserved for more than {stayLimit} days.");
        }
        private async void PreventOverbooking(Guid roomId, DateOnly startDate, DateOnly endDate)
        {
            var overlapingReservations = await _reservationRepository.ListActiveByRoomAndDateRangeOverlap(roomId, startDate, endDate);
            if (overlapingReservations.Count > 0)
                throw new OverbookingException($"Selected room is already reserved for the given period.");

        }
    }
}
