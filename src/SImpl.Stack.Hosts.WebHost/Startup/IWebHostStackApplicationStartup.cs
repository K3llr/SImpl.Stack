using SImpl.Stack.ApplicationBuilders;

namespace SImpl.Stack.Hosts.WebHost.Startup
{
    public interface IWebHostStackApplicationStartup
    {
        void ConfigureStackApplication(IWebHostApplicationBuilder configureDelegate);
    }
}