namespace HealthySystem.Data.Models
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser
    {
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
