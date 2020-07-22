using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace HealthySystem.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
