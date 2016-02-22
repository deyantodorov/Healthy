namespace HealthySystem.Web.Areas.Identity.ViewModels.Manage
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Common;

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = ModelConstants.Required)]
        [DataType(DataType.Password)]
        [Display(Name = "Текуща парола")]
        [UIHint("SingleLineTextPasswordTiny")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = ModelConstants.Required)]
        [StringLength(100, ErrorMessage = ModelConstants.StringLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Нова парола")]
        [UIHint("SingleLineTextPasswordTiny")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повтори нова парола")]
        [Compare("NewPassword", ErrorMessage = ModelConstants.ConfirmPassword)]
        [UIHint("SingleLineTextPasswordTiny")]
        public string ConfirmPassword { get; set; }
    }
}