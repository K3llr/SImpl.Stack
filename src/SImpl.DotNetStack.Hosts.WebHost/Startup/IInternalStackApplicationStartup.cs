using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.WebHost.ApplicationBuilder;

namespace SImpl.DotNetStack.Hosts.WebHost.Startup
{
    public interface IInternalStackApplicationStartup
    {
        void ConfigureServices(IServiceCollection services);

        void ConfigureStackApplication(IWebHostApplicationBuilder builder);
    }
}