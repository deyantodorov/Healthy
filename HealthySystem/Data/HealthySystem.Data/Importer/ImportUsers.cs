namespace HealthySystem.Data.Importer
{
    using System.Linq;
    using HealthySystem.Common;
    using HealthySystem.Data.Importer.Contracts;
    using HealthySystem.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ImportUsers : IDataImporter
    {
        public void Import(ApplicationDbContext context)
        {
            this.CreateUser(context, WebConstants.AdminUserName, WebConstants.AdminEmail, WebConstants.AdminPassword, WebConstants.AdminRole);
            this.CreateUser(context, WebConstants.EditorUserName, WebConstants.EditorEmail, WebConstants.EditorPassword, WebConstants.EditorRole);
        }

        private void CreateUser(ApplicationDbContext context, string userName, string userEmail, string userPass, string userRole)
        {
            if (context.Users.Any(u => u.UserName == userName))
            {
                return;
            }

            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store)
            {
                PasswordValidator = new PasswordValidator
                {
                    RequiredLength = WebConstants.MinUserPasswordLength,
                    RequireNonLetterOrDigit = false,
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                }
            };

            var user = new User
            {
                UserName = userName,
                Email = userEmail
            };

            manager.Create(user, userPass);
            manager.AddToRole(user.Id, userRole);
        }
    }
}
