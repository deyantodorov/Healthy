using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using HealthySystem.Data.Common.Models;

namespace HealthySystem.Data.Models
{
    public class Tag : BaseModel<int>
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Alias { get; set; }

        public int Rank { get; set; }

        public ICollection<TagArticles> TagArticles { get; set; } = new HashSet<TagArticles>();
    }
}