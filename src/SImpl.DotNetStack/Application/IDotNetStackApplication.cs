using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SImpl.DotNetStack.Application
{
    public interface IDotNetStackApplication
    {
        void ConfigureService(IServiceCollection services);
        
        Task StartAsync();
        
        Task StopAsync();
    }
}