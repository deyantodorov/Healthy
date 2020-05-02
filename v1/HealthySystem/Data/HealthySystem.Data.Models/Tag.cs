namespace HealthySystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using HealthySystem.Data.Common.Models;

    public class Tag : BaseModel<int>
    {
        private ICollection<Article> articles;

        public Tag()
        {
            this.articles = new HashSet<Article>();
        }

        [Required]
        [StringLength(150)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        [Index(IsUnique = true)]
        public string Alias { get; set; }

        public int Rank { get; set; }

        [ForeignKey("ArticleId")]
        public virtual ICollection<Article> Articles
        {
            get { return this.articles; }
            set { this.articles = value; }
        }
    }
}