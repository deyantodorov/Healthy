﻿namespace HealthySystem.Web.Areas.Manager.ViewModels.Image
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using HealthySystem.Common;
    using HealthySystem.Web.Infrastructure.CustomValidators;

    public class ImageViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ModelConstants.Required)]
        [Display(Name = "Добави картинка")]
        [FileSize(3000000)]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase ImagePath { get; set; }

        [Required(ErrorMessage = ModelConstants.Required)]
        [StringLength(250)]
        [UIHint("SingleLineText")]
        [Display(Name = "Описание")]
        public string ImageDescription { get; set; }
    }
}