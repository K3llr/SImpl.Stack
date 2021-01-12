using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SImpl.DotNetStack.ApplicationBuilders;

namespace SImpl.DotNetStack.Application
{
    public interface IApplicationBootManager
    {
        void Configure(IDotNetStackApplicationBuilder appBuilder);
        
        void ConfigureServices(IServiceCollection serviceCollection);

        Task StartAsync();

        Task StopAsync();
    }
}