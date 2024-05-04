using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pro1.Models
{
    public class Callendar
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; } // The date of the calendar event

        [Required]
        [ForeignKey("Ticket")] // Reference to the Ticket model
        public int TicketId { get; set; }

        [Required]
        [ForeignKey("Employee")] // Reference to the Employee model
        public int EmployeeId { get; set; }

        [Required]
        [Range(0, 23)] // Restrict the range to valid hours
        public int Hour { get; set; }
    }
}
