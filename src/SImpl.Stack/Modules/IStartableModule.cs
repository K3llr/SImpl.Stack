using System.Threading.Tasks;

namespace SImpl.Stack.Modules
{
    public interface IStartableModule : IDotNetStackModule
    {
        Task StartAsync();
        Task StopAsync();
    }
}