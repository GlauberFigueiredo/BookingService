using AutoMapper;
using BookingService.Model.Entities;
using BookingService.Model.Enums;
using BookingService.Model.ViewModels;
using BookingService.Respository.Contracts;
using BookingService.Service.Contracts;
using BookingService.Shared;
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

        public ReservationService(IReservationRepository reservationRepository
                                  ,IMapper mapper)
            : base(mapper)
        {
            this._reservationRepository = reservationRepository;
        }


        public async Task<List<ReservationViewModel>> GetAll()
        {
            var reservations = await _reservationRepository.GetAll();
            var result = _mapper.Map<List<Reservation>, List<ReservationViewModel>>(reservations);
            return result;
        }

        public async Task<bool> IsAvailable(DateOnly startDate, DateOnly endDate)
        {
            ValidateSelectedDates(startDate, endDate);
            try
            {
                await PreventOverbooking(startDate, endDate);
            }
            catch (OverbookingException)
            {
                return false;
            }
            return true;
        }

        public async Task<ReservationViewModel> CreateNew(DateOnly startDate, DateOnly endDate)
        {
            ValidateSelectedDates(startDate, endDate);
            await PreventOverbooking(startDate, endDate);

            var newReservation = new Reservation(startDate, endDate);
            var reservationEntity = await _reservationRepository.Create(newReservation);

            var result = _mapper.Map<Reservation, ReservationViewModel>(reservationEntity);

            return result;
        }

        public async Task<ReservationViewModel> Update(Guid reservationId, DateOnly startDate, DateOnly endDate)
        {
            var selectedReservation = await _reservationRepository.GetById(reservationId);
            ValidateReservation(selectedReservation);
            ValidateSelectedDates(startDate, endDate);
            await PreventOverbooking(startDate, endDate);

            selectedReservation.StartDate = startDate;
            selectedReservation.EndDate = endDate;

            var reservationEntity = await _reservationRepository.Update(selectedReservation);

            var result = _mapper.Map<Reservation, ReservationViewModel>(reservationEntity);

            return result;
        }

        public async Task<ReservationViewModel> Cancel(Guid reservationId)
        {
            var selectedReservation = await _reservationRepository.GetById(reservationId);
            ValidateReservation(selectedReservation);
            selectedReservation.Status = ReservationStatus.CANCELLED;

            var reservationEntity = await _reservationRepository.Update(selectedReservation);

            var result = _mapper.Map<Reservation, ReservationViewModel>(reservationEntity);

            return result;
        }


        #region Private Methods
        private void ValidateReservation(Reservation reservation)
        {
            if (reservation == null)
                throw new ReservationValidationException("No reservation was found for the informed Id.");
            if (reservation.Status != ReservationStatus.ACTIVE)
                throw new ReservationValidationException("The informed reservation has already been canceled.");
        }
        private void ValidateSelectedDates(DateOnly startDate, DateOnly endDate)
        {
            var stayLimit = 3;
            var advanceReservationlimit = 30;

            var today = DateOnly.FromDateTime(DateTime.Now);

            if ((startDate.DayNumber - today.DayNumber) < 1)
                throw new DateValidationException("All reservations must start at least the next day of booking.");
            if (endDate.DayNumber < startDate.DayNumber)
                throw new DateValidationException("The end date must be greater than the start date.");
            if ((startDate.DayNumber - today.DayNumber) > advanceReservationlimit)
                throw new DateValidationException($"Rooms can`t be reserved more than {advanceReservationlimit} days in advance.");
            if ((endDate.DayNumber - startDate.DayNumber) > stayLimit)
                throw new DateValidationException($"Rooms can`t be reserved for more than {stayLimit} days.");
        }
        private async Task PreventOverbooking(DateOnly startDate, DateOnly endDate)
        {
            var overlapingReservations = await _reservationRepository.ListActiveByRoomAndDateRangeOverlap(startDate, endDate);
            if (overlapingReservations.Count > 0)
                throw new OverbookingException($"Room is already booked during the given period.");
        }
        #endregion
    }
}
