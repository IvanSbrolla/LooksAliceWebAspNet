using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LooksAliceWebAspNet.Startup))]
namespace LooksAliceWebAspNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
