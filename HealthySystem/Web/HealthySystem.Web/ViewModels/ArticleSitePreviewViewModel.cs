namespace HealthySystem.Web.ViewModels
{
    using AutoMapper;
    using HealthySystem.Data.Models;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class ArticleSitePreviewViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Alias { get; set; }

        public string Image { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleSitePreviewViewModel>()
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => "r/" + src.Rubric.Alias + "/" + src.Alias))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.ImagePath));
        }
    }
}