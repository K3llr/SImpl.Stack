using System.Threading.Tasks;

namespace SImpl.DotNetStack.Application
{
    public interface IDotNetStackApplication
    {
        Task StartAsync();
        
        Task StopAsync();
    }
}