namespace HealthySystem.Data.Models
{
    public class TagArticles
    {
        public int TagId { get; set; }

        public Tag Tag { get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
