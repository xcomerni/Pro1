using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pro1.Models
{
    public class Parts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int TicketId { get; set; }
        [Required]
        public decimal unitPrice { get; set; }
        [Required]
        public int amount { get; set; }

    }
}
