namespace HealthySystem.Web.Areas.Manager.ViewModels.Image
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Data.Models;
    using Infrastructure.Mapping;

    public class ImageListViewModel : IMapFrom<Image>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[UIHint("UploadFile")]
        [Display(Name = "Добави картинка")]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(250)]
        [UIHint("SingleLineText")]
        [Display(Name = "Описание")]
        public string ImageDescription { get; set; }
    }
}