namespace HealthySystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Data.Common.Models;

    public class Image : BaseModel<int>
    {
        [Required]
        [MaxLength(250)]
        public string ImagePath { get; set; }

        [Required]
        [MaxLength(250)]
        public string ImageDescription { get; set; }
    }
}