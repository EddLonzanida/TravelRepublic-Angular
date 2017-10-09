using Microsoft.AspNet.Identity.EntityFramework;
using TravelRepublic.Contracts.Infrastructure;
using TravelRepublic.Data.Security;

namespace TravelRepublic.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(ConnectionStrings.TravelRepublicKey, throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
