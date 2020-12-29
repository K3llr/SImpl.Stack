using System.Threading.Tasks;

namespace Novicell.DotNetStack.Modules
{
    public interface IStartableModule : IDotNetStackModule
    {
        Task StartAsync();
        Task StopAsync();
    }
}