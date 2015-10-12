using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineCV.Startup))]
namespace OnlineCV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
