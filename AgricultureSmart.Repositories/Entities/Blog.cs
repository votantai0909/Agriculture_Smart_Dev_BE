using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Repositories.Entities
{
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Content { get; set; }

        [StringLength(500)]
        public string FeaturedImage { get; set; }

        [Required]
        [StringLength(500)]
        public string Slug { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } // "draft", "published", "archived"

        public int ViewCount { get; set; }

        public DateTime? PublishedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        [ForeignKey("AuthorId")]
        public virtual Users Author { get; set; }

        [ForeignKey("CategoryId")]
        public virtual BlogCategory Category { get; set; }

    }
}
