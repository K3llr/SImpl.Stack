using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Hosts.GenericHost.ApplicationBuilders
{
    public interface IGenericStackApplicationBuilder : IDotNetStackApplicationBuilder
    {
        void Configure();

        void ConfigureServices(IServiceCollection services);
    }
}