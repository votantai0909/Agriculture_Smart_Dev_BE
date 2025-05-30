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
    public class Farmer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [StringLength(255)]
        public string FarmLocation { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal FarmSize { get; set; } // in hectares

        [Column(TypeName = "nvarchar(max)")]
        public string CropTypes { get; set; } // JSON string for crop types

        public int FarmingExperienceYears { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }

        public virtual ICollection<EngineerFarmerAssignment> EngineerFarmerAssignments { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

    }
}
