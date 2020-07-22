using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using HealthySystem.Data.Common.Models;

namespace HealthySystem.Data.Models
{
    public class Comment : BaseModel<int>
    {
        [Required]
        [MinLength(2)]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        [Required]
        public int ArticleId { get; set; }

        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
    }
}