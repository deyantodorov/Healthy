namespace HealthySystem.Web.ViewModels
{
    using HealthySystem.Data.Models;
    using HealthySystem.Web.Infrastructure.Mapping;

    public class TagSiteViewModel : IMapFrom<Tag>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public int Rank { get; set; }
    }
}