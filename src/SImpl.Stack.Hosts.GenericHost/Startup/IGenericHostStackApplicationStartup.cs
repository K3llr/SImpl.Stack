using Microsoft.Extensions.DependencyInjection;
using SImpl.Stack.ApplicationBuilders;

namespace SImpl.Stack.Hosts.GenericHost.Startup
{
    public interface IGenericHostStackApplicationStartup
    {
        void ConfigureServices(IServiceCollection services);

        void ConfigureStackApplication(IGenericHostApplicationBuilder builder);
    }
}