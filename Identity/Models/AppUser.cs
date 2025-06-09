using Microsoft.AspNetCore.Identity;

namespace Identity.Models
{
    public class AppUser: IdentityUser
    {
        public string? FullName { get; set; }
    }
}
