using AutoMapper;
using BookingService.Model.Entities;
using BookingService.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Model.MappingProfiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(dst => dst.StartDate, opt => opt.MapFrom(s => s.StartDate.ToString()))
                .ForMember(dst => dst.EndDate, opt => opt.MapFrom(s => s.EndDate.ToString()))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                .ForMember(dst => dst.RoomId, opt => opt.MapFrom(s => s.Room.Id.ToString()))
                .ForMember(dst => dst.RoomNumber, opt => opt.MapFrom(s => s.Room.Number.ToString()))
                .ForMember(dst => dst.Floor, opt => opt.MapFrom(s => s.Room.Floor.ToString()))
                ;
        }
    }
}
