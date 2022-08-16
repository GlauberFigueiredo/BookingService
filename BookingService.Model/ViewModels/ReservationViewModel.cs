using BookingService.Model.Entities;
using BookingService.Model.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BookingService.Model.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class ReservationViewModel
    {
        [JsonProperty("StartDate")]
        public string StartDate { get; set; }
        [JsonProperty("EndDate")]
        public string EndDate { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }
    }
}