using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Entities
{
    public class EngineerFarmerAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int EngineerId { get; set; }

        [Required]
        public int FarmerId { get; set; }

        public DateTime AssignedAt { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Notes { get; set; }

        // Navigation properties
        [ForeignKey("EngineerId")]
        public virtual Engineer Engineer { get; set; }

        [ForeignKey("FarmerId")]
        public virtual Farmer Farmer { get; set; }

    }
}
