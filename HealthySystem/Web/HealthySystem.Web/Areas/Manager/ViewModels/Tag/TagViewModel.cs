namespace HealthySystem.Web.Areas.Manager.ViewModels.Tag
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Data.Models;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Име")]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "URL")]
        [UIHint("SingleLineText")]
        public string Alias { get; set; }

        [Required]
        public int Rank { get; set; }
    }
}