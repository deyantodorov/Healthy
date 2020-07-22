namespace HealthySystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HealthySystem.Data.Common.Models;

    public class Rubric : BaseModel<int>
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        public string Alias { get; set; }

        [MaxLength(150)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        public int? ParentId { get; set; }
    }
}
