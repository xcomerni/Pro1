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
        [ForeignKey("Employee")]
        public int? EmployeeId { get; set; }
        public string State = "created";
        public string? EstimateDescription { get; set; }
        public decimal? EstimatePrice { get; set;}
        public bool IsAccepted = false;
        public decimal PricePaid = 0;



    }
}
