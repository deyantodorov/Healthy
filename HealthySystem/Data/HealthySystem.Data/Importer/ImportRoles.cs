namespace HealthySystem.Data.Importer
{
    using System.Linq;
    using HealthySystem.Common;
    using HealthySystem.Data.Importer.Contracts;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ImportRoles : IContentImporter
    {
        public void Import(ApplicationDbContext context)
        {
            this.CreateRole(context, WebConstants.AdminRole);
            this.CreateRole(context, WebConstants.EditorRole);
        }

        private void CreateRole(ApplicationDbContext context, string roleName)
        {
            if (context.Roles.Any(r => r.Name == roleName))
            {
                return;
            }

            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var role = new IdentityRole
            {
                Name = roleName
            };

            manager.Create(role);
        }
    }
}