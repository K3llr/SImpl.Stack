using System.Threading.Tasks;

namespace SImpl.Modules
{
    public interface IStartableModule : ISImplModule
    {
        Task StartAsync();
        Task StopAsync();
    }
}