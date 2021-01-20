using System.Threading.Tasks;

namespace SImpl.DotNetStack.ApplicationBuilders
{
    public interface IDotNetStackApplication
    {
        Task StartAsync();
        
        Task StopAsync();
    }
}