using AutoMapper;
using BookingService.Model.Entities;
using BookingService.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Model.MappingProfiles
{
    [ExcludeFromCodeCoverage]
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(dst => dst.StartDate, opt => opt.MapFrom(s => s.StartDate.ToString()))
                .ForMember(dst => dst.EndDate, opt => opt.MapFrom(s => s.EndDate.ToString()))
                .ForMember(dst => dst.Status, opt => opt.MapFrom(s => s.Status.ToString()))
                ;
        }
    }
}
