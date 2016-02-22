namespace HealthySystem.Web.Areas.Identity.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Common;

    public class LoginViewModel
    {
        [Required(ErrorMessage = ModelConstants.Required)]
        [Display(Name = "Потребителско име")]
        [UIHint("SingleLineTextTiny")]
        public string UserName { get; set; }

        [Required(ErrorMessage = ModelConstants.Required)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        [UIHint("SingleLineTextPasswordTiny")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
