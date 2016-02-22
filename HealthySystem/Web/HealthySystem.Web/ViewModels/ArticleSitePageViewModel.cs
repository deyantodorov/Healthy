namespace HealthySystem.Web.ViewModels
{
    using System;
    using System.Web.Mvc;
    using AutoMapper;
    using HealthySystem.Common;
    using HealthySystem.Data.Models;
    using HealthySystem.Web.Infrastructure.Images;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class ArticleSitePageViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Description { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Image { get; set; }

        public string ImageCache => Images.GetImageFromCache(this.Image, WebConstants.ImageWidth, WebConstants.ImageMaxHeight);

        public DateTime Date { get; set; }

        public string Alias { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleSitePageViewModel>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image.ImagePath))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.PublishDate))
                .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => "r/" + src.Rubric.Alias + "/" + src.Alias));
        }
    }
}