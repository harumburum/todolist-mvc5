using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TodoList.Web.Startup))]
namespace TodoList.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
