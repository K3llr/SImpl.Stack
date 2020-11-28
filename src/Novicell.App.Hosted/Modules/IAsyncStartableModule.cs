using System.Threading.Tasks;

namespace Novicell.App.Hosted.Modules
{
    public interface IAsyncStartableModule
    {
        Task StartAsync();
        Task StopAsync();
    }
}