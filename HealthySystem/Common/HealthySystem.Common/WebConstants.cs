namespace HealthySystem.Common
{
    public class WebConstants
    {
        // MVC settings
        public const string DefaultLoginPath = "/Identity/Account/Login";
        public const int MinUserPasswordLength = 6;

        // Administration settings
        public const string ManagerAreaName = "Management System";
        public const string AdminRole = "Administrator";
        public const string EditorRole = "Editor";

        // Default users
        // TODO: Change in production
        public const string AdminUserName = "Deyan";
        public const string AdminEmail = "deyan@gmail.com";
        public const string AdminPassword = "123123";

        // Directories
        public const string DirectoryUpload = "Uploads";
        public const string DirectoryCache = "Cache";
    }
}