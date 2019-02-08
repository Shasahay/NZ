using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NotesZone.Web.Startup))]
namespace NotesZone.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
