using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders;

namespace SImpl.DotNetStack.Hosts.GenericHost.Startup
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder app);
    }
}