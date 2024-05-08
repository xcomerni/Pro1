using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pro1.Models
{
    public class Callendar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.Today;// The date of the calendar event
        [Required]
        [ForeignKey("Employee")] // Reference to the Employee model
        public int EmployeeId { get; set; }

        [Required]
        [Range(0, 23)] // Restrict the range to valid hours
        public int Hour { get; set; } 
        [Required]
        public int TicketId { get; set; }

    }
}
