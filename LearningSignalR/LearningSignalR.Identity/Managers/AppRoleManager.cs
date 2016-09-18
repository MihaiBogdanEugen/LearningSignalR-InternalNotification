using LearningSignalR.Db;
using LearningSignalR.Db.Models.Identity;
using LearningSignalR.Identity.Stores;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace LearningSignalR.Identity.Managers
{
    public class AppRoleManager : RoleManager<Role, long>
    {
        public AppRoleManager(IRoleStore<Role, long> roleStore) : base(roleStore) { }

        public static AppRoleManager Create(IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            return new AppRoleManager(new AppRoleStore(context.Get<AppDbContext>()));
        }
    }
}