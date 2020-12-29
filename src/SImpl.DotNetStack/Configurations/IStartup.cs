using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Application
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);

        void Configure(IDotNetStackApplicationBuilder stackBuilder);
    }
}