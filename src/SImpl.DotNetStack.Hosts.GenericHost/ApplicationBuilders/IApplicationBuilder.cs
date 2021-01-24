using Microsoft.Extensions.DependencyInjection;

namespace SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders
{
    public interface IApplicationBuilder
    {
        void Configure();
        
        void ConfigureServices(IServiceCollection services);
    }
}