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
            if (!context.Users.Any(u => u.UserName == WebConstants.AdminUserName))
            {
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
                    UserName = WebConstants.AdminUserName,
                    Email = WebConstants.AdminEmail
                };

                manager.Create(user, WebConstants.AdminPassword);
                manager.AddToRole(user.Id, WebConstants.AdminRole);
            }
        }
    }
}
