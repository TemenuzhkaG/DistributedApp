using Microsoft.AspNet.Identity.EntityFramework;

namespace WebAPI.Infrastructure.Data
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext()
          : base("AuthDbContext")
        {

        }
    }
}
