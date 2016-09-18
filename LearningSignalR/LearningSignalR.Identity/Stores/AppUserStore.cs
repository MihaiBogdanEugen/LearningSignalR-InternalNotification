using System.Data.Entity;
using LearningSignalR.Db.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LearningSignalR.Identity.Stores
{
    public class AppUserStore : UserStore<User, Role, long, UserLogin, UserRole, UserClaim>
    {
        public AppUserStore(DbContext context) : base(context) { }

        protected override void Dispose(bool disposing)
        {
            if (base.DisposeContext)
                base.Dispose(disposing);
        }
    }
}