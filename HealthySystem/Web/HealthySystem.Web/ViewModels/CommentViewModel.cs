namespace HealthySystem.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using HealthySystem.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        [Required(ErrorMessage = ModelConstants.Required)]
        [Display(Name = "Коментар")]
        [StringLength(1000, ErrorMessage = ModelConstants.StringLength, MinimumLength = 2)]
        [UIHint("MultiLineTextWide")]
        public string Content { get; set; }

        public string Author { get; set; }

        [Required]
        public string ArticleAlias { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author.UserName));
        }
    }
}