using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using HealthySystem.Data.Common.Models;

namespace HealthySystem.Data.Models
{
    public class Article : BaseModel<int>
    {
        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(150)]
        public string Alias { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public bool IsPublished { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public DateTime? UnpublishDate { get; set; }

        public int Rank { get; set; }

        public int? ImageId { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }

        [Required]
        public int RubricId { get; set; }

        [ForeignKey("RubricId")]
        public Rubric Rubric { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }

        public ICollection<TagArticles> TagArticles { get; set; } = new HashSet<TagArticles>();

        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}