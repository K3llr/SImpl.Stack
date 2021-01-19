using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.GenericHost.ApplicationBuilders;

namespace SImpl.DotNetStack.GenericHost.Configuration
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);

        void Configure(IApplicationBuilder app);
    }
}