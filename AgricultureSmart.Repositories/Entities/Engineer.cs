using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Entities
{
    public class Engineer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [StringLength(255)]
        public string Specialization { get; set; }

        public int ExperienceYears { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Certification { get; set; } // JSON string for certifications

        [Column(TypeName = "nvarchar(max)")]
        public string Bio { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        public virtual ICollection<EngineerFarmerAssignment> EngineerFarmerAssignments { get; set; }

        public virtual ICollection<Ticket> AssignedTickets { get; set; }

    }
}
