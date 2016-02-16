namespace HealthySystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Data.Common.Models;

    public class Rubric : BaseModel<int>
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Alias { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? ParentId { get; set; }
    }
}
