using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.HostBuilders;

namespace SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders
{
    public interface IApplicationBuilder
    {
        IDotNetStackHostBuilder HostBuilder { get; }

        void Configure();
        
        void ConfigureServices(IServiceCollection services);
    }
}