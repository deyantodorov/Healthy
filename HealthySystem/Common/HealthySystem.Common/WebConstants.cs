namespace HealthySystem.Common
{
    public class WebConstants
    {
        // Domain settings
        public const string Domain = "http://megazdrave.com";
        public const string DomainTitle = "MegaZdrave.com";

        // MVC settings
        public const string DefaultLoginPath = "/Identity/Account/Login";
        public const int MinUserPasswordLength = 6;

        // Administration settings
        public const string ManagerAreaName = "Management System";
        public const string AdminRole = "Administrator";
        public const string EditorRole = "Editor";
        public const int AdminPageSize = 20;
        public const int AdminPagingSize = 10;
        public const string AdminMenuTitleFacebook = "Facebook";
        public const string AdminMenuTitleGooglePlus = "Google Plus";
        public const string AdminMenuTitleGoogleAnalytics = "Google Analytics";
        public const string AdminMenuTitleManageDelete = "Управление на изтрито съдържание";

        // Cache time in secconds 30 min = 30 * 60 = 1800 seconds
        public const int AdminCacheTime = 1800;
        public const string CacheRubricFromDb = "CacheRubricFromDb";

        // Default users
        public const string AdminUserName = "Admin";
        public const string AdminEmail = "admin@gmail.com";
        public const string AdminPassword = "admin123123";
        public const string EditorUserName = "Editor";
        public const string EditorEmail = "editor@gmail.com";
        public const string EditorPassword = "editor123123";

        // Directories
        public const string DirectoryUpload = "Uploads";
        public const string DirectoryCache = "Cache";

        // Site Cache
        public const string SiteCacheArticlesHome = "SiteArticlesHome";
        public const string SiteCacheTags = "SiteTags";
        public const string SiteCacheArticlesSiteMap = "ArticlesSiteMap";

        // Site listings
        public const int SiteNumberOfTags = 120;
        public const int SiteHomePageSize = 18;
        public const int SiteRubricPageSize = 14;
        public const int SiteSearchPageSize = 18;
        public const int SiteOtherArticlesSize = 8;

        // Cache time in secconds 15 min = 15 * 60 = 900 seconds
        public const int Min60 = 3600;
        public const int Min15 = 900;
        public const int Min5 = 300;

        // Images
        public const int ImageWidth = 1000;
        public const int ImageMaxHeight = 1000;

        // Article
        public const int ArticleTextSplitLength = 1000;
        public const int ArticleTextSplitLengthExtra = 100;
    }
}