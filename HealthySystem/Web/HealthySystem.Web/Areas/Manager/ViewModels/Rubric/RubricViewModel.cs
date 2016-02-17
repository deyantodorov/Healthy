namespace HealthySystem.Web.Areas.Manager.ViewModels.Rubric
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Data.Models;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class RubricViewModel : IMapFrom<Rubric>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Име")]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Display(Name = "Основна рубрика")]
        [UIHint("SingleLineText")]
        public int? ParentId { get; set; }

        [Required]
        [Display(Name = "URL")]
        [UIHint("SingleLineText")]
        public string Alias { get; set; }
    }
}