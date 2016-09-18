using LearningSignalR;
using LearningSignalR.Infrastructure;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Startup))]
namespace LearningSignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.ConfigureOwinContext();
            app.ConfigureAuth();
            app.MapSignalR();
        }
    }
}
