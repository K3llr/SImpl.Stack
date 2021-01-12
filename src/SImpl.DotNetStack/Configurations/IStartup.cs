using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Configurations
{
    public interface IStartup : IStartup<IDotNetStackApplicationBuilder>
    {
    }
    
    public interface IStartup<in TApplicationBuilder>
        where TApplicationBuilder : IDotNetStackApplicationBuilder
    {
        void ConfigureServices(IServiceCollection services);

        void Configure(TApplicationBuilder applicationBuilder);
    }
}