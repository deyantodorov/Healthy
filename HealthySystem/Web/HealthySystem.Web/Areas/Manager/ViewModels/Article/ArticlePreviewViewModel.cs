namespace HealthySystem.Web.Areas.Manager.ViewModels.Article
{
    using System;
    using AutoMapper;
    using HealthySystem.Data.Models;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class ArticlePreviewViewModel : IMapFrom<Article>, IMapTo<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string RubricName { get; set; }
        
        // TODO: REMOVE
        public DateTime AddDate { get; set; }

        public DateTime? PublishDate { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticlePreviewViewModel>()
                .ForMember(dest => dest.RubricName, opt => opt.MapFrom(src => src.Rubric.Name))
                .ReverseMap();
        }
    }
}