namespace HealthySystem.Web.Areas.Manager.ViewModels.Article
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using HealthySystem.Web.Infrastructure.Mapping;
    using System.Text;
    using HealthySystem.Data.Models;

    public class ArticleViewModel : IMapFrom<Article>, IMapTo<Article>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [UIHint("SingleLineText")]
        [Display(Name = "Заглавие")]
        [StringLength(150, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Описание")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        [AllowHtml]
        [Required]
        [Display(Name = "Статия")]
        [DataType("TinyMce")]
        [UIHint("TinyMce")]
        public string Content { get; set; }

        [Display(Name = "Публикувай")]
        [UIHint("CheckBox")]
        public bool IsPublished { get; set; }

        [Required]
        [Display(Name = "Дата на добавяне")]
        [UIHint("SingleLineText")]
        public DateTime AddDate { get; set; }

        [Required]
        [Display(Name = "Дата на публикуване")]
        [UIHint("SingleLineText")]
        public DateTime? PublishDate { get; set; }

        [Display(Name = "Дата на скриване")]
        [UIHint("SingleLineText")]
        public DateTime? UnpublishDate { get; set; }

        public int? ImageId { get; set; }

        [Required]
        [Display(Name = "URL")]
        [UIHint("SingleLineText")]
        [StringLength(150)]
        public string Alias { get; set; }

        [Display(Name = "Категория")]
        [UIHint("DropDownList")]
        public int RubricId { get; set; }

        public IEnumerable<SelectListItem> Rubrics { get; set; }

        [Required]
        [Display(Name = "Автор")]
        [UIHint("SingleLineText")]
        [StringLength(128)]
        public string AuthorName { get; set; }

        [Display(Name = "Тагове")]
        [UIHint("SingleLineText")]
        public string Tags { get; set; }

        public string ReferrerUrl { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleViewModel>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Id))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => this.GenerateTags(src.Tags)))
                .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.ImageId))
                .ReverseMap();
        }

        private string GenerateTags(ICollection<Tag> tags)
        {
            var sb = new StringBuilder();

            var index = 0;

            foreach (var item in tags)
            {
                if (index == tags.Count() - 1)
                {
                    sb.Append(item.Name);
                }
                else
                {
                    sb.Append(item.Name + ", ");
                }
            }

            return sb.ToString();
        }
    }
}