using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;

namespace SImpl.DotNetStack.Hosts.GenericHost.Startup
{
    public interface IGenericHostStackApplicationStartup
    {
        void ConfigureServices(IServiceCollection services);

        void ConfigureStackApplication(IGenericHostApplicationBuilder builder);
    }
}