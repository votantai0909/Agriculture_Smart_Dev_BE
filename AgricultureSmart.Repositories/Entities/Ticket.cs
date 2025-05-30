using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Entities
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int FarmerId { get; set; }

        public int? AssignedEngineerId { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // "open", "assigned", "in_progress", "resolved", "closed"

        [Required]
        [StringLength(20)]
        public string Priority { get; set; } // "low", "medium", "high", "urgent"

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }

        // Navigation properties
        [ForeignKey("FarmerId")]
        public virtual Farmer Farmer { get; set; }

        [ForeignKey("AssignedEngineerId")]
        public virtual Engineer AssignedEngineer { get; set; }

        public virtual ICollection<TicketComment> Comments { get; set; }

    }
}
