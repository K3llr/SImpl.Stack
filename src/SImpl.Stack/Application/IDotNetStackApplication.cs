using System.Threading.Tasks;

namespace SImpl.Stack.Application
{
    public interface IDotNetStackApplication
    {
        Task StartAsync();
        
        Task StopAsync();
    }
}