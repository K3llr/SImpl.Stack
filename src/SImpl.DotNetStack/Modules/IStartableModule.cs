using System.Threading.Tasks;

namespace SImpl.DotNetStack.Modules
{
    public interface IStartableModule : IDotNetStackModule
    {
        Task StartAsync();
        Task StopAsync();
    }
}