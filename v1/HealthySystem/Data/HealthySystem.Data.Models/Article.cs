namespace HealthySystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using HealthySystem.Data.Common.Models;

    public class Article : BaseModel<int>
    {
        private ICollection<Tag> tags;
        private ICollection<Comment> comments;

        public Article()
        {
            this.tags = new HashSet<Tag>();
            this.comments = new HashSet<Comment>();
        }

        [Required]
        [StringLength(150)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        [Index(IsUnique = true)]
        public string Alias { get; set; }

        [Required]
        [StringLength(250)]
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
        public virtual Image Image { get; set; }

        [Required]
        public int RubricId { get; set; }

        [ForeignKey("RubricId")]
        public virtual Rubric Rubric { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual User Author { get; set; }

        [ForeignKey("TagId")]
        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}