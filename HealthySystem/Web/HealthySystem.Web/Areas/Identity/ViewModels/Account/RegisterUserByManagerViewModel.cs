namespace HealthySystem.Web.Areas.Identity.ViewModels.Account
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Common;

    public class RegisterUserByManagerViewModel
    {
        [Required(ErrorMessage = ModelConstants.Required)]
        [Display(Name = "Потребителско име")]
        [StringLength(16, MinimumLength = 5)]
        [UIHint("SingleLineText")]
        public string UserName { get; set; }

        [Required(ErrorMessage = ModelConstants.Required)]
        [EmailAddress]
        [Display(Name = "Е-майл")]
        [UIHint("SingleLineText")]
        public string Email { get; set; }

        [Required(ErrorMessage = ModelConstants.Required)]
        [StringLength(100, ErrorMessage = ModelConstants.StringLength, MinimumLength = WebConstants.MinUserPasswordLength)]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        [UIHint("SingleLineTextPassword")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потвърди паролата")]
        [Compare("Password", ErrorMessage = ModelConstants.ConfirmPassword)]
        [UIHint("SingleLineTextPassword")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Роля")]
        [UIHint("DropDownList")]
        public string UserRoleId { get; set; }

        public IEnumerable<System.Web.Mvc.SelectListItem> UserRoles { get; set; }
    }
}