namespace HealthySystem.Web.Areas.Identity.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Common;

    public class RegisterViewModel
    {
        [Required(ErrorMessage = ModelConstants.Required)]
        [EmailAddress]
        [Display(Name = "Е-мейл")]
        [UIHint("SingleLineTextTiny")]
        public string Email { get; set; }

        [Required(ErrorMessage = ModelConstants.Required)]
        [StringLength(100, ErrorMessage = ModelConstants.StringLength, MinimumLength = WebConstants.MinUserPasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        [UIHint("SingleLineTextPasswordTiny")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди парола")]
        [Compare("Password", ErrorMessage = ModelConstants.ConfirmPassword)]
        [UIHint("SingleLineTextPasswordTiny")]
        public string ConfirmPassword { get; set; }
    }
}