using Microsoft.Extensions.DependencyInjection;
using SImpl.Stack.ApplicationBuilders;

namespace SImpl.Stack.Hosts.WebHost.Startup
{
    public interface IInternalStackApplicationStartup
    {
        void ConfigureServices(IServiceCollection services);

        void ConfigureStackApplication(IWebHostApplicationBuilder builder);
    }
}