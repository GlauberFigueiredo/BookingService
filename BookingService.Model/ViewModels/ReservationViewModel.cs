using BookingService.Model.Entities;
using BookingService.Model.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Model.ViewModels
{
    public class ReservationViewModel
    {
        [JsonProperty("Id")]
        public Guid Id { get; set; }
        [JsonProperty("StartDate")]
        public string StartDate { get; set; }
        [JsonProperty("EndDate")]
        public string EndDate { get; set; }
        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("RoomId")]
        public String RoomId { get; set; }
        [JsonProperty("RoomNumber")]
        public String RoomNumber { get; set; }
        [JsonProperty("Floor")]
        public String Floor { get; set; }

    }
}