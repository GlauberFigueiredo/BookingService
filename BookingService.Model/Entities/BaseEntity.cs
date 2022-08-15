
using BookingService.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Model.Entities
{
    public class BaseEntity
    {
        [Column("Id")]
        public Guid Id { get; set; }
    }
}