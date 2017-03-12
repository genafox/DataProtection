using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebServer.Startup))]

namespace WebServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
