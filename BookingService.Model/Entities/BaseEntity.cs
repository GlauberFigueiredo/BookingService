
using BookingService.Model.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace BookingService.Model.Entities
{
    [ExcludeFromCodeCoverage]
    public class BaseEntity
    {
        [Column("Id")]
        public Guid Id { get; set; }
    }
}