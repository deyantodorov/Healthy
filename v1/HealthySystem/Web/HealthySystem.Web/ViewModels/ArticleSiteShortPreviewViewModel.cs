namespace HealthySystem.Web.ViewModels
{
    using AutoMapper;
    using HealthySystem.Data.Models;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class ArticleSiteShortPreviewViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Alias { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleSiteShortPreviewViewModel>()
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => "r/" + src.Rubric.Alias + "/" + src.Alias));
        }
    }
}