using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Pro1.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Registration { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int TimeSlots { get; set; }
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public string State { get; set; } = "created";
        public string? EstimateDescription { get; set; }
        public decimal EstimatePrice { get; set;} = decimal.Zero;
        public bool IsAccepted { get; set; } = false;
        public decimal PricePaid { get; set; } = decimal.Zero;

    }
}
