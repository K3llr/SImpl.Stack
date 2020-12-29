using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Novicell.DotNetStack.Application
{
    public interface IDotNetStackApplication
    {
        void ConfigureService(IServiceCollection services);
        
        Task StartAsync();
        
        Task StopAsync();
    }
}