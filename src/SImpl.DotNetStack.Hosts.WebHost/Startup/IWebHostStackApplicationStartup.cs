using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;

namespace SImpl.DotNetStack.Hosts.WebHost.Startup
{
    public interface IWebHostStackApplicationStartup
    {
        void ConfigureStackApplication(IWebHostApplicationBuilder configureDelegate);
    }
}