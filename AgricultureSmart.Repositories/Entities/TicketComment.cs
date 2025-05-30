using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Entities
{
    public class TicketComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TicketId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Comment { get; set; }

        public bool IsInternal { get; set; } // For engineer-only notes

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }

        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

    }
}
