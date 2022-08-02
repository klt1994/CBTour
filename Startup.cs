using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CBTour.Startup))]
namespace CBTour
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
