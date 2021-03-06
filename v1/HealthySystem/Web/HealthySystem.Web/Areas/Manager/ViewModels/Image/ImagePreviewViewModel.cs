﻿namespace HealthySystem.Web.Areas.Manager.ViewModels.Image
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Data.Models;
    using Infrastructure.Mapping;

    public class ImagePreviewViewModel : IMapFrom<Image>, IMapTo<Image>
    {
        [Key]
        public int Id { get; set; }

        // [UIHint("UploadFile")]
        [Required]
        [Display(Name = "Добави картинка")]
        public string ImagePath { get; set; }

        [Required]
        [StringLength(250)]
        [UIHint("SingleLineText")]
        [Display(Name = "Описание")]
        public string ImageDescription { get; set; }
    }
}