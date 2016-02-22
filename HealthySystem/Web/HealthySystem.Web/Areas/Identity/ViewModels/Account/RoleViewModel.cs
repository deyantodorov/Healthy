namespace HealthySystem.Web.Areas.Identity.ViewModels.Account
{
    using System.ComponentModel.DataAnnotations;
    using HealthySystem.Common;
    using HealthySystem.Web.Infrastructure.Mapping;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class RoleViewModel : IMapFrom<IdentityRole>, IMapTo<IdentityRole>
    {
        [Required(ErrorMessage = ModelConstants.Required)]
        [StringLength(30, MinimumLength = 3)]
        [Display(Name = "Роля")]
        [UIHint("SingleLineText")]
        public string Name { get; set; }
    }
}