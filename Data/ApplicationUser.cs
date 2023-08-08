using Microsoft.AspNetCore.Identity;

namespace NewIdentityApp.Data
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsEnabled { get; set; }
    }
}
