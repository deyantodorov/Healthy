namespace HealthySystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using HealthySystem.Common;
    using HealthySystem.Services.Data.Contracts;
    using HealthySystem.Web.Infrastructure.Mapping;
    using HealthySystem.Web.ViewModels;

    public abstract class SiteController : BaseController
    {
        public ITagService TagService { get; set; }

        protected virtual IList<TagSiteViewModel> GetTags()
        {
            var tags = this.Cache.Get(
                WebConstants.SiteCacheTags,
                () => this.TagService.GetAll()
                .OrderByDescending(x => x.Rank)
                .Take(WebConstants.SiteNumberOfTags)
                .To<TagSiteViewModel>()
                .ToList(),
                WebConstants.Min15);

            return tags;
        }
    }
}