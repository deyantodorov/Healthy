namespace HealthySystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using HealthySystem.Data.Common.Models;

    public class Rubric : BaseModel<int>
    {
        [Required]
        [StringLength(150)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        [Index(IsUnique = true)]
        public string Alias { get; set; }

        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? ParentId { get; set; }
    }
}
